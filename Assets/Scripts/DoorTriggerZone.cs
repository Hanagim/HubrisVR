using UnityEngine;
using DG.Tweening;

public class DoorTriggerZone : MonoBehaviour
{
    [Tooltip("The door to open when the player enters this trigger.")]
    public Transform doorMesh;

    public Vector3 slideOffset = new Vector3(0, -3f, 0);  // Example: slides down
    public float slideDuration = 0.5f;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool hasOpened = false;
    private Tween currentTween;

    private void Start()
    {
        if (doorMesh == null)
        {
            Debug.LogError("DoorTriggerZone: doorMesh not assigned!");
            return;
        }

        closedPosition = doorMesh.localPosition;
        openPosition = closedPosition + slideOffset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasOpened) return;

        if (other.CompareTag("Player"))
        {
            hasOpened = true;
            currentTween?.Kill();
            currentTween = doorMesh.DOLocalMove(openPosition, slideDuration).SetEase(Ease.OutQuad);
        }
    }
}
