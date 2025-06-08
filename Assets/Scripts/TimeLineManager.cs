using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System;

public class TimelineManager : MonoBehaviour
{
    public static TimelineManager Instance;

    [System.Serializable]
    public class TimelineEntry
    {
        public string timelineID;
        public PlayableDirector director;
    }

    [Header("List of Timelines")]
    public List<TimelineEntry> timelineEntries = new List<TimelineEntry>();

    private Dictionary<string, PlayableDirector> timelineDict;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        timelineDict = new Dictionary<string, PlayableDirector>();
        foreach (var entry in timelineEntries)
        {
            if (!timelineDict.ContainsKey(entry.timelineID))
                timelineDict.Add(entry.timelineID, entry.director);
        }
    }

    private void Start()
    {
        PlayTimeline("intro"); // Automatically start the intro
    }

    // ✅ Default call to play a timeline
    public void PlayTimeline(string id)
    {
        PlayTimeline(id, null);
    }

    // ✅ Overload with callback
    public void PlayTimeline(string id, Action onComplete)
    {
        if (timelineDict.TryGetValue(id, out var director))
        {
            director.Stop(); // Just in case it was running
            director.Play();

            if (onComplete != null)
            {
                // Remove previous listeners just in case
                director.stopped -= OnTimelineStopped;
                director.stopped += OnTimelineStopped;

                // Closure to pass callback
                void OnTimelineStopped(PlayableDirector d)
                {
                    if (d == director)
                    {
                        director.stopped -= OnTimelineStopped;
                        onComplete.Invoke();
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning($"Timeline '{id}' not found.");
        }
    }

    public void StopTimeline(string id)
    {
        if (timelineDict.TryGetValue(id, out var director))
        {
            director.Stop();
        }
        else
        {
            Debug.LogWarning($"Timeline '{id}' not found.");
        }
    }

    public void StopAllTimelines()
    {
        foreach (var director in timelineDict.Values)
        {
            if (director.state == PlayState.Playing)
                director.Stop();
        }
    }

    public void Transition(string fromID, string toID)
    {
        StopTimeline(fromID);
        PlayTimeline(toID);
    }
}
