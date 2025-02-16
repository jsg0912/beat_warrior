using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Events;
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
                            Debug.Log($"씬 변경 후 Signal Receiver 바인딩 성공: {receiver.gameObject.name}");
                            SystemMessageUIManager.Instance.isTimeLinePlaying = true;
                            return; 
                        }
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("타임라인 또는 Signal Receiver를 찾을 수 없음!");
        }
    }
}