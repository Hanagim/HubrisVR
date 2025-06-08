using UnityEngine;

public class DoorProximityTrigger : MonoBehaviour
{
    public float triggerDistance = 2f;

    private Animator doorAnimator;
    private Transform player;
    private Transform selfTransform;

    private void Awake()
    {
        selfTransform = transform;
        doorAnimator = GetComponent<Animator>(); // Ensures we use the local Animator

        // Automatically find the player if not assigned
        if (Camera.main != null)
        {
            player = Camera.main.transform; // Works for XR and standard
        }
        else
        {
            Debug.LogWarning("Player (Camera.main) not found. Assign manually if needed.");
        }
    }

    private void Update()
    {
        if (doorAnimator == null || player == null)
            return;

        float distance = Vector3.Distance(player.position, selfTransform.position);
        bool isNearby = distance <= triggerDistance;

        doorAnimator.SetBool("CharacterNearby", isNearby);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, triggerDistance);
    }
}
