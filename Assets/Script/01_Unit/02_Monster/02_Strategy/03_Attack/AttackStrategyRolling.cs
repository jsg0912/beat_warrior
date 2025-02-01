using DG.Tweening;
using UnityEngine;

public class AttackStrategyRolling : AttackStrategy
{
    public float jumpPower = 3f; // 점프 높이
    public int numJumps = 1;  // 튀는 횟수
    public float duration = 1f; // 전체 애니메이션 시간

    protected override void SkillMethod()
    {
        float rotateAngle = -1980 * numJumps;
        Transform target = Player.Instance.transform;
        monster.transform.DOJump(target.position, jumpPower, numJumps, duration)
            .SetEase(Ease.OutQuad) // 부드러운 튀는 느낌
            .OnComplete(() => Debug.Log("점프 완료!"));
        monster.transform.DORotate(new Vector3(0, 0, rotateAngle), duration, RotateMode.FastBeyond360)
        .SetEase(Ease.Linear); // 일정한 속도로 회전
    }
}