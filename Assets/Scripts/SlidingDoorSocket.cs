using UnityEngine;
using DG.Tweening;

public class SlidingDoorSocket : MonoBehaviour
{
    public Transform doorMesh;               // The door's visual part
    public Vector3 slideOffset;              // Movement offset (e.g., down on Y)
    public float slideDuration = 0.5f;       // Time to open/close
    public string requiredTag = "Key";       // Tag of the object required to open

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private Tween currentTween;

    private void Start()
    {
        if (doorMesh == null)
            doorMesh = transform;

        closedPosition = doorMesh.localPosition;
        openPosition = closedPosition + slideOffset;
    }

    // Call this from the socket's OnSocketed UnityEvent
    public void TryOpen(GameObject socketedObject)
    {
        if (socketedObject.CompareTag(requiredTag))
        {
            currentTween?.Kill();
            currentTween = doorMesh.DOLocalMove(openPosition, slideDuration).SetEase(Ease.OutQuad);
        }
    }

    // Optional: Call this from OnUnSocketed if you want the door to close again
    public void TryClose(GameObject unSocketedObject)
    {
        if (unSocketedObject.CompareTag(requiredTag))
        {
            currentTween?.Kill();
            currentTween = doorMesh.DOLocalMove(closedPosition, slideDuration).SetEase(Ease.OutQuad);
        }
    }
}
