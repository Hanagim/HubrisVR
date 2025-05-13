using UnityEngine;
using DG.Tweening;

public class SlidingDoorTrigger : MonoBehaviour
{
    public Transform doorMesh; // The visible part that moves
    public float slideDistance = 2f; // How far the door moves down
    public float slideDuration = 0.5f;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private Tween currentTween;

    private void Start()
    {
        if (doorMesh == null) doorMesh = transform; // Default to self
        closedPosition = doorMesh.localPosition;
        openPosition = closedPosition - new Vector3(0, slideDistance, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        currentTween?.Kill();
        currentTween = doorMesh.DOLocalMove(openPosition, slideDuration).SetEase(Ease.OutQuad);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        currentTween?.Kill();
        currentTween = doorMesh.DOLocalMove(closedPosition, slideDuration).SetEase(Ease.OutQuad);
    }
}
