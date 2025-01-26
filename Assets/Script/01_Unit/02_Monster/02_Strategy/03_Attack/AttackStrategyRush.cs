using System.Collections;
using UnityEngine;

public class AttackStrategyRush : AttackStrategy
{
    protected float rushSpeed;
    protected Direction RushDirection;
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
        monster.attackCollider.SetActive(true);
        SetRushDirection();

        while (elapsedTime < dashDuration)
        {
            elapsedTime += Time.deltaTime;
            Rush();
            yield return null;
        }

        monster.SetIsFixedAnimation(false);
        monster.SetIsTackleAble(false);
        monster.SetIsKnockBackAble(true);
        monster.attackCollider.SetActive(false);
        attackCoolTime = attackCoolTimeMax;

        monster.PlayAnimation(MonsterStatus.AttackEnd);
        monster.SetStatus(MonsterStatus.Idle);
    }

    protected virtual void SetRushDirection()
    {
        if (Player.Instance.transform.position.x > monster.transform.position.x) RushDirection = Direction.Right;
        else RushDirection = Direction.Left;
    }

    protected virtual void Rush()
    {
        if (isChangingDir) return;

        monster.SetMovingDirection(RushDirection);

        CheckWall();
        CheckGround();

        monster.gameObject.transform.position += new Vector3((int)RushDirection * rushSpeed * Time.deltaTime, 0, 0);
    }

    protected virtual void CheckWall()
    {
        Vector3 start = GetMonsterMiddlePos() + new Vector3(GetMonsterSize().x / 2, 0, 0) * (int)RushDirection;
        Vector3 dir = Vector3.right * (int)RushDirection;

        RaycastHit2D rayHit = Physics2D.Raycast(start, dir, 0.1f, LayerMask.GetMask(LayerConstant.Tile));
        //Debug.DrawLine(start, start + dir * 0.1f, Color.red);
        if (rayHit.collider != null && rayHit.collider.CompareTag("Base"))
        {
            monoBehaviour.StartCoroutine(ChangeDir());
        }
    }

    protected Vector3 GetRayStartPoint()
    {
        return GetMonsterPos() + new Vector3((int)RushDirection * GetMonsterSize().x / 2, 0, 0);
    }

    protected void CheckGround()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(GetRayStartPoint(), Vector3.down, 0.1f, GroundLayer);
        //Debug.DrawLine(GetRayStartPoint(), GetMonsterPos() + offset + Vector3.down * 0.1f, Color.red);

        if (rayHit.collider == null) monoBehaviour.StartCoroutine(ChangeDir());
    }

    protected IEnumerator ChangeDir()
    {
        RushDirection = (Direction)(-1 * (int)RushDirection);
        monster.SetMovingDirection(RushDirection);
        monster.SetIsFixedAnimation(false);
        monster.PlayAnimation(MonsterConstant.turnAnimTrigger);
        monster.SetIsFixedAnimation(true);
        isChangingDir = true;
        yield return new WaitForSeconds(0.33f);
        isChangingDir = false;
    }
}
