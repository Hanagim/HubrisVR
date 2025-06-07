using UnityEngine;
using DG.Tweening;

public class SlidingDoorTriggerFromOther : MonoBehaviour
{
    public Transform doorMesh;           // The part that moves
    public Vector3 slideOffset;          // Movement offset (e.g., slide up/down)
    public float slideDuration = 0.5f;   // Time to open

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private Tween currentTween;
    private bool doorOpened = false;

    private void Start()
    {
        if (doorMesh == null) doorMesh = transform;
        closedPosition = doorMesh.localPosition;
        openPosition = closedPosition + slideOffset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (doorOpened) return;

        if (other.CompareTag("DoorTrigger"))
        {
            // Check if the player is inside this "DoorTrigger" collider
            Collider[] overlapping = Physics.OverlapBox(
                other.bounds.center,
                other.bounds.extents,
                other.transform.rotation);

            foreach (var hit in overlapping)
            {
                if (hit.CompareTag("Player"))
                {
                    OpenDoor();
                    doorOpened = true;
                    break;
                }
            }
        }
    }

    private void OpenDoor()
    {
        currentTween?.Kill();
        currentTween = doorMesh.DOLocalMove(openPosition, slideDuration).SetEase(Ease.OutQuad);
    }
}
