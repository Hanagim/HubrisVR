using UnityEngine;

public class LeverTrigger : MonoBehaviour
{
    public Transform lever;
    public float confirmThreshold = 45f; // Z rotation trigger
    public PuzzleManager puzzleManager;

    private bool wasTriggered;

    void Update()
    {
        float z = lever.localEulerAngles.z;
        if (z > confirmThreshold && !wasTriggered)
        {
            wasTriggered = true;
            puzzleManager.TrySolve();
        }
        if (z < confirmThreshold - 10f && wasTriggered)
        {
            wasTriggered = false;
        }
    }
}
