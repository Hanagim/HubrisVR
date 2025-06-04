using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    [Header("Shader")]
    public Renderer[] waterRenderers; // All 3 tank renderers
    private Material[] materialInstances;

    [Header("RGB Wheels")]
    public Transform wheelR;
    public Transform wheelG;
    public Transform wheelB;
    public TextMeshProUGUI rgbText;
    public TextMeshProUGUI rText;
    public TextMeshProUGUI gText;
    public TextMeshProUGUI bText;

    [Header("Dials (Fill)")]
    public Transform[] dials; // 3 dials
    public TextMeshProUGUI[] fillTexts;

    [Header("Target Fills (0–100)")]
    public float[] targetFills = new float[3]; // e.g., {65, 40, 90}
    public float fillTolerance = 2f; // in 0–100 space

    [Header("Target RGB Color (0–255)")]
    public int targetR = 51;
    public int targetG = 102;
    public int targetB = 255;
    public int colorTolerance = 10;

    [Header("On Puzzle Solved")]
    public UnityEvent onSolved;

    private float[] currentFills = new float[3];
    private int currentR, currentG, currentB;

    void Start()
    {
        materialInstances = new Material[waterRenderers.Length];
        for (int i = 0; i < waterRenderers.Length; i++)
            materialInstances[i] = waterRenderers[i].material;
    }

    void Update()
    {
        // Read RGB wheels
        float r = Mathf.Clamp01(wheelR.localEulerAngles.z / 360f);
        float g = Mathf.Clamp01(wheelG.localEulerAngles.z / 360f);
        float b = Mathf.Clamp01(wheelB.localEulerAngles.z / 360f);

        currentR = Mathf.RoundToInt(r * 255);
        currentG = Mathf.RoundToInt(g * 255);
        currentB = Mathf.RoundToInt(b * 255);

        Color currentColor = new Color(r, g, b);
        foreach (var mat in materialInstances)
            mat.SetColor("_SideColor", currentColor);

        rgbText.SetText($"Color RGB:\n{currentR}, {currentG}, {currentB}");
        rText.SetText($"{currentR}");
        gText.SetText($"{currentG}");
        bText.SetText($"{currentB}");

        // Read each fill dial
        for (int i = 0; i < dials.Length; i++)
        {
            float angle = dials[i].localEulerAngles.y;
            float fill = Mathf.Clamp01(angle / 360f);
            currentFills[i] = fill * 100f; // Convert to 0–100
            materialInstances[i].SetFloat("_Fill", fill);

            if (fillTexts != null && fillTexts.Length > i)
                fillTexts[i].SetText($"Fill Level:\n{Mathf.RoundToInt(currentFills[i])}%");
        }
    }

    public void TrySolve()
    {
        bool colorMatch =
            Mathf.Abs(currentR - targetR) <= colorTolerance &&
            Mathf.Abs(currentG - targetG) <= colorTolerance &&
            Mathf.Abs(currentB - targetB) <= colorTolerance;

        bool allFillsMatch = true;
        for (int i = 0; i < targetFills.Length; i++)
        {
            if (Mathf.Abs(currentFills[i] - targetFills[i]) > fillTolerance)
            {
                allFillsMatch = false;
                break;
            }
        }

        if (colorMatch && allFillsMatch)
        {
            Debug.Log("Puzzle Solved!");
            onSolved.Invoke();
        }
        else
        {
            Debug.Log("Incorrect combination.");
        }
    }
}
