using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    [Header("Shader")]
    public Renderer waterRenderer;
    private Material materialInstance;

    [Header("RGB Wheels")]
    public Transform wheelR;
    public Transform wheelG;
    public Transform wheelB;
    public TextMeshProUGUI rgbText;

    [Header("Dial (Fill)")]
    public Transform dial;
    public TextMeshProUGUI fillText;

    [Header("Target Fill (0-1)")]
    public float targetFill = 0.65f;
    public float fillTolerance = 0.02f;
    [Header("Target RGB Color (0-255)")]
    public int targetR = 51;   // Example: 0.2 * 255
    public int targetG = 102;  // Example: 0.4 * 255
    public int targetB = 255;  // Example: 1.0 * 255

    public int colorTolerance = 10;  // Acceptable margin (in RGB range)

    [Header("On Puzzle Solved")]
    public UnityEvent onSolved;

    private float currentFill;
    private int currentR, currentG, currentB;

    void Start()
    {
        materialInstance = waterRenderer.material;
    }

    void Update()
    {
        // RGB from wheels
            // RGB from wheels (Z rotation → 0–1 → 0–255)
            float r = Mathf.Clamp01(wheelR.localEulerAngles.z / 360f);
            float g = Mathf.Clamp01(wheelG.localEulerAngles.z / 360f);
            float b = Mathf.Clamp01(wheelB.localEulerAngles.z / 360f);

            currentR = Mathf.RoundToInt(r * 255);
            currentG = Mathf.RoundToInt(g * 255);
            currentB = Mathf.RoundToInt(b * 255);

            Color currentColor = new Color(r, g, b);
            materialInstance.SetColor("_SideColor", currentColor);
            rgbText.SetText($"Color RGB:\n{currentR}, {currentG}, {currentB}");

            // Fill from dial (Y rotation)
            float angle = dial.localEulerAngles.y;
            currentFill = Mathf.Clamp01(angle / 360f);
            materialInstance.SetFloat("_Fill", currentFill);
            fillText.SetText($"Fill Level:\n{Mathf.RoundToInt(currentFill * 100)}%");
    }

    public void TrySolve()
    {
        bool colorMatch =
        Mathf.Abs(currentR - targetR) <= colorTolerance &&
        Mathf.Abs(currentG - targetG) <= colorTolerance &&
        Mathf.Abs(currentB - targetB) <= colorTolerance;

        bool fillMatch = Mathf.Abs(currentFill - targetFill) < fillTolerance;

        if (colorMatch && fillMatch)
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
