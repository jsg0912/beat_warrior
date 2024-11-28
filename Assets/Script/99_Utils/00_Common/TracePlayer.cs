using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracePlayer : MonoBehaviour
{
    private Transform Target;


    private void Start()
    {
        Target = Player.Instance.transform;
    }

    private void LateUpdate()
    {
        Target = Player.Instance.transform;
        TraceToPlayer();

    }

    private void TraceToPlayer()
    {
        // 타겟의 위치를 직접 카메라 위치로 설정
        transform.position = new Vector3(Target.position.x, Target.position.y + 0.5f, 0);
    }

}
