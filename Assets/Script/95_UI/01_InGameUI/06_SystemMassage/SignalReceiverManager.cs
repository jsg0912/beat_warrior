using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;

public class SignalReceiverManager : MonoBehaviour
{
    public GameObject signalReceiverObject;
    private PlayableDirector timeline;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        FindAndBindTimeline();
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindAndBindTimeline();
    }

    void FindAndBindTimeline()
    {
        timeline = FindObjectOfType<PlayableDirector>();
        if (timeline != null && signalReceiverObject != null)
        {
            var receiver = signalReceiverObject.GetComponent<SignalReceiver>();
            if (receiver != null)
            {

                TimelineAsset timelineAsset = timeline.playableAsset as TimelineAsset;
                if (timelineAsset != null)
                {
                    foreach (var track in timelineAsset.GetOutputTracks())
                    {
                        if (track is SignalTrack)
                        {
                            timeline.SetGenericBinding(track, receiver);
                            SystemMessageUIManager.Instance.isTimeLinePlaying = true;
                            return;
                        }
                    }
                }
            }
        }
    }
}