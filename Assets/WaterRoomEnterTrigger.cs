using UnityEngine;
using UnityEngine.Playables;

public class WaterRoomEnterTrigger : MonoBehaviour
{
    public string timelineID = "WaterRoomIntro";

    public bool disableAfterTrigger = true;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag("Player"))
            return;

        triggered = true;
        TimelineManager.Instance.StopAllTimelinesExcept(timelineID);
        TimelineManager.Instance.PlayTimeline(timelineID);


        if (disableAfterTrigger)
            gameObject.SetActive(false);
    }
}
