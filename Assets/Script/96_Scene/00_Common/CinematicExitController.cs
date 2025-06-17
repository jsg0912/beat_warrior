using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicExitController : MonoBehaviour
{
    public PlayableDirector director;
    public MoveCamera moveCamera;
    public Transform player;

    void Start()
    {
        director.stopped += OnTimelineStopped;
    }

    private void OnTimelineStopped(PlayableDirector pd)
    {
        Debug.Log("Timeline ³¡");

        moveCamera.enabled = true;
        moveCamera.Target = player;

        /*moveCamera.transform.position = new Vector3(
            player.position.x,
            player.position.y + moveCamera.yOffest,
            moveCamera.z
        );*/
    }
}
