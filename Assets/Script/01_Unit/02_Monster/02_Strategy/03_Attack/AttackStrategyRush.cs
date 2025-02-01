using System.Collections;
using UnityEngine;

public class AttackStrategyRush : AttackStrategy
{
    protected float rushSpeed;
    protected float dashDuration;
    protected Coroutine rushCoroutine;
    protected LayerMask GroundLayer;
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

    protected override IEnumerator UseSkill()
    {
        monster.SetStatus(MonsterStatus.Attack);
        SetAttackDirection();
        yield return new WaitForSeconds(attackStartDelay);

        monster.PlayAnimation(MonsterStatus.AttackCharge);
        yield return new WaitForSeconds(attackActionInterval);
        monster.PlayAnimation(MonsterStatus.Attack);
        SkillMethod();
    }

    protected override void SkillMethod()
    {
        attackCoroutine = monoBehaviour.StartCoroutine(RushCoroutine());
    }

    protected IEnumerator RushCoroutine()
    {
        float elapsedTime = 0f;
        monster.SetIsTackleAble(true);
        monster.SetIsKnockBackAble(false);
        monster.SetIsFixedAnimation(true);

        while (elapsedTime < dashDuration)
        {
            elapsedTime += Time.deltaTime;
            Rush();
            yield return null;
        }

        monster.SetIsFixedAnimation(false);
        monster.SetIsTackleAble(false);
        monster.SetIsKnockBackAble(true);
        attackCoolTime = attackCoolTimeMax;

        monster.PlayAnimation(MonsterStatus.AttackEnd);
        monster.SetStatus(MonsterStatus.Idle);
    }

    protected virtual void Rush()
    {
        if (isChangingDir) return;

        monster.SetMovingDirection(attackDirection);

        CheckWall();
        CheckGround();

        monster.gameObject.transform.position += new Vector3((int)attackDirection * rushSpeed * Time.deltaTime, 0, 0);
    }

    //[Code Review - KMJ] TODO: MoveStrategy에도 있는데, Wall을 Check해서 bool을 return하는 함수를 만들고, 그에 따라 필요한 행동은 Strategy에서 알아서 하도록 수정 필요, CheckGround도 마찬가지 - Nights, 20250201
    protected virtual void CheckWall()
    {
        float movingDirection = GetMovingDirectionFloat();
        Vector3 start = GetMonsterMiddleFrontPos();
        Vector3 dir = Vector3.right * movingDirection;

        RaycastHit2D rayHit = Physics2D.Raycast(start, dir, MonsterConstant.WallCheckRayDistance, LayerMask.GetMask(LayerConstant.Tile));
        // Debug.DrawLine(start, start + dir * MonsterConstant.WallCheckDistance, Color.red);
        if (rayHit.collider != null && rayHit.collider.CompareTag(TagConstant.Base))
        {
            monoBehaviour.StartCoroutine(ChangeDir());
        }
    }

    protected Vector3 GetGroundCheckRayStartPoint()
    {
        return GetMonsterPos() + new Vector3((int)attackDirection * GetMonsterSize().x / 2, 0, 0);
    }

    protected void CheckGround()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(GetMonsterFrontPos() + new Vector3(0, 0.05f, 0), Vector3.down, MonsterConstant.GroundCheckRayDistance, GroundLayer);
        //Debug.DrawLine(GetMonsterFrontPos(), GetMonsterFrontPos() + Vector3.down * MonsterConstant.GroundCheckRayDistance, Color.red);

        if (rayHit.collider == null) monoBehaviour.StartCoroutine(ChangeDir());
    }

    protected IEnumerator ChangeDir()
    {
        DebugConsole.Log($"attackDirection: {attackDirection}");
        monster.FlipDirection();
        attackDirection = monster.GetMovingDirection();
        DebugConsole.Log($"attackDirection: {attackDirection}");
        monster.SetIsFixedAnimation(false);
        monster.PlayAnimation(MonsterConstant.turnAnimTrigger);
        monster.SetIsFixedAnimation(true);
        isChangingDir = true;
        yield return new WaitForSeconds(0.5f);
        isChangingDir = false;
    }
}
