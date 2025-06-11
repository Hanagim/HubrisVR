using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    [Header("Hint System")]
    public AudioClip hintVoiceLine;
    public float firstHintDelay = 120f;
    public float repeatHintInterval = 60f;

    private float timeSincePuzzleStarted = 0f;
    private bool puzzleSolved = false;
    private float nextHintTime = 0f;
    private bool puzzleStarted = false;

    [Header("Wheels (Fill Control)")]
    public Transform[] wheels; // 6 wheels, A–F
    public TextMeshProUGUI[] fillTexts; // 6 fill percentage texts
    public Renderer[] waterRenderers; // 6 tanks with shader

    [Header("Target Fills (0–100)")]
    public float[] targetFills = new float[6]; // A–F target fill percentages
    public float fillTolerance = 2f;

    [Header("On Puzzle Solved")]
    public UnityEvent onSolved;

    [Header("Incorrect Attempt Sound")]
    public AudioClip incorrectSound;
    private AudioSource audioSource;

    private float[] currentFills = new float[6];
    private Material[] materialInstances;

    void Start()
    {
        // Setup materials
        materialInstances = new Material[waterRenderers.Length];
        for (int i = 0; i < waterRenderers.Length; i++)
            materialInstances[i] = waterRenderers[i].material;

        // Setup audio
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (!puzzleStarted || puzzleSolved)
            return;

        timeSincePuzzleStarted += Time.deltaTime;

        if (timeSincePuzzleStarted >= nextHintTime)
        {
            if (hintVoiceLine != null && audioSource != null)
            {
                audioSource.PlayOneShot(hintVoiceLine);
                Debug.Log("Hint played at: " + timeSincePuzzleStarted + " seconds");
            }

            nextHintTime += repeatHintInterval;
        }

        for (int i = 0; i < wheels.Length; i++)
        {
            float angle = wheels[i].localEulerAngles.z;
            float normalized = Mathf.Clamp01(angle / 360f);
            float fill = normalized * 100f;

            currentFills[i] = fill;

            if (materialInstances != null && i < materialInstances.Length)
                materialInstances[i].SetFloat("_Fill", normalized);

            if (fillTexts != null && i < fillTexts.Length)
                fillTexts[i].SetText($"{Mathf.RoundToInt(fill)}%");
        }
    }

    public void StartPuzzle()
    {
        if (!puzzleStarted)
        {
            puzzleStarted = true;
            timeSincePuzzleStarted = 0f;
            nextHintTime = firstHintDelay;
            Debug.Log("Puzzle started.");
        }
    }

    public void TrySolve()
    {
        if (puzzleSolved)
            return;

        bool allMatch = true;
        for (int i = 0; i < targetFills.Length; i++)
        {
            if (Mathf.Abs(currentFills[i] - targetFills[i]) > fillTolerance)
            {
                allMatch = false;
                break;
            }
        }

        if (allMatch)
        {
            Debug.Log("Puzzle Solved!");
            puzzleSolved = true;
            onSolved.Invoke();
        }
        else
        {
            Debug.Log("Incorrect combination.");
            if (incorrectSound != null && audioSource != null)
                audioSource.PlayOneShot(incorrectSound);
        }
    }
}
