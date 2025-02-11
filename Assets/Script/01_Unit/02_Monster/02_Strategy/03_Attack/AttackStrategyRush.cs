using System.Collections;
using UnityEngine;

public class AttackStrategyRush : AttackStrategy
{
    protected float rushSpeed;
    protected float dashDuration;
    protected Coroutine rushCoroutine;
    protected bool isChangingDir = false;

    public AttackStrategyRush(float rushSpeed, float dashDuration)
    {
        this.rushSpeed = rushSpeed;
        this.dashDuration = dashDuration;
    }

    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);
        GroundLayer = LayerMask.GetMask(LayerConstant.Tile);
    }

    protected override void SkillMethod()
    {
        attackCoroutine = monoBehaviour.StartCoroutine(RushCoroutine());

        monster.SetIsTackleAble(true);
        monster.SetIsKnockBackAble(false);
        monster.SetIsFixedAnimation(true);
        Util.SetActive(monster.attackCollider, true);
    }

    protected IEnumerator RushCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            elapsedTime += Time.deltaTime;
            Rush();
            yield return null;
        }

        monster.SetIsFixedAnimation(false);
        monster.PlayAnimation(MonsterConstant.attackEndAnimTrigger);
    }

    public override void AttackEnd()
    {
        base.AttackEnd();

        monster.SetIsTackleAble(false);
        monster.SetIsKnockBackAble(true);
        Util.SetActive(monster.attackCollider, false);
    }

    protected virtual void Rush()
    {
        if (isChangingDir) return;

        monster.SetMovingDirection(attackDirection);

        if (CheckWall() || CheckEndOfGround()) monoBehaviour.StartCoroutine(ChangeDir());

        monster.gameObject.transform.position += new Vector3((int)attackDirection * rushSpeed * Time.deltaTime, 0, 0);
    }

    protected IEnumerator ChangeDir()
    {
        monster.FlipDirection();
        attackDirection = monster.GetMovingDirection();

        monster.SetIsFixedAnimation(false);
        monster.PlayAnimation(MonsterConstant.turnAnimTrigger);
        monster.SetIsFixedAnimation(true);

        isChangingDir = true;
        yield return new WaitForSeconds(0.33f);
        isChangingDir = false;
    }
}
