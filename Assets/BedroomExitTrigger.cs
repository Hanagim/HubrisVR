using UnityEngine;
using UnityEngine.Playables;

public class BedroomExitTrigger : MonoBehaviour
{
    public string firstTimelineID = "LightsToMainRoom";
    public string chainedTimelineID = "AVEIntro";
    public string finalTimelineID = "AVEToWaterRoom";

    public bool disableAfterTrigger = true;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            TimelineManager.Instance.PlayTimeline(firstTimelineID, () => {
                TimelineManager.Instance.PlayTimeline(chainedTimelineID, () => {
                    TimelineManager.Instance.PlayTimeline(finalTimelineID);
                });
            });

            if (disableAfterTrigger)
                gameObject.SetActive(false);
        }
    }
}
