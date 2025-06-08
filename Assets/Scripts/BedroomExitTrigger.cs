using UnityEngine;

public class BedroomExitTrigger : MonoBehaviour
{
    [Tooltip("Timeline ID to play when the player exits the bedroom.")]
    public string timelineID;

    [Tooltip("Should this trigger be disabled after firing once?")]
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
