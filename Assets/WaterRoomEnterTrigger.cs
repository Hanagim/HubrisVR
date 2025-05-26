using UnityEngine;
using UnityEngine.Playables;

public class WaterRoomEnterTrigger : MonoBehaviour
{
    public string firstTimelineID = "WaterRoomIntro";

    public bool disableAfterTrigger = true;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            TimelineManager.Instance.PlayTimeline(firstTimelineID, () => {
            });

            if (disableAfterTrigger)
                gameObject.SetActive(false);
        }
    }
}
