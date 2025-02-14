using System.Collections;
using UnityEngine;

public class AttackStrategyRush : AttackStrategy
{
    protected float rushSpeed;
    protected float dashDuration;
    protected float dashDurationLeft;
    protected Coroutine rushCoroutine;
    protected bool isChangingDir = false;

    public AttackStrategyRush(float rushSpeed, float dashDuration)
    {
        this.rushSpeed = rushSpeed;
        this.dashDuration = dashDuration;
        dashDurationLeft = dashDuration;
    }

    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);
        GroundLayer = LayerMask.GetMask(LayerConstant.Tile);
    }

    protected override void AttackMethod()
    {
        monster.SetIsFixedAnimation(true);
        Util.SetActive(monster.attackCollider, true);
    }

    public override void AttackUpdate()
    {
        if (isChangingDir) return;
        if (dashDurationLeft <= 0)
        {
            dashDurationLeft = dashDuration;
            monster.SetIsFixedAnimation(false);
            monster.PlayAnimation(MonsterConstant.attackEndAnimTrigger);
        }

        dashDurationLeft -= Time.deltaTime;
        monster.SetMovingDirection(attackDirection);
        MoveFor(GetMovingDirection(), rushSpeed);

        if (CheckWall() || CheckEndOfGround()) monoBehaviour.StartCoroutine(ChangeDir());
    }

    public override void AttackEnd()
    {
        base.AttackEnd();

        Util.SetActive(monster.attackCollider, false); // Rush는 턴도는 동작 때문에 여기서 Collider 비활성화
    }

    protected IEnumerator ChangeDir()
    {
        float turningTime = 0.33f;
        monster.FlipDirection();
        attackDirection = monster.GetMovingDirection();

        monster.SetIsFixedAnimation(false);
        monster.PlayAnimation(MonsterConstant.turnAnimTrigger);
        monster.SetIsFixedAnimation(true);

        isChangingDir = true;
        yield return new WaitForSeconds(turningTime);
        dashDurationLeft -= turningTime;
        isChangingDir = false;
    }
}
