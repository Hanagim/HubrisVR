using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    public PuzzleManager puzzleManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            puzzleManager.StartPuzzle();
        }
    }
}
