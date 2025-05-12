using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 3f;      // How close the player must be
    public float speed = 2f;               // Speed of sliding
    public Vector3 openOffset = new Vector3(0, -3, 0); // How far door moves down

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpening = false;

    void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + openOffset;

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        isOpening = distance < detectionRange;

        Vector3 targetPosition = isOpening ? openPosition : closedPosition;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
    }
}
