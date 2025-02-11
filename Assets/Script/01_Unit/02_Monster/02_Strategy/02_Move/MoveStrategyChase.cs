using UnityEngine;

public class MoveStrategyChase : MoveStrategy
{
    private const float STOPOFFSET = 1f;

    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);

        moveSpeed = MonsterConstant.MoveSpeed[monster.monsterName];
    }

    public override bool PlayStrategy()
    {
        ChaseTarget();

        return base.PlayStrategy();
    }

    protected Vector3 TargetPos()
    {
        return Player.Instance.transform.position;
    }

    protected void ChaseTarget()
    {
        SetMovingDirection(monster.GetRelativeDirectionToPlayer());
    }

    protected override bool IsMoveable()
    {
        if (CheckWall() || CheckEndOfGround() || Mathf.Abs(TargetPos().x - GetMonsterPos().x) < STOPOFFSET)
        {
            return false;
        }

        return monster.GetIsMoveable();
    }
}
