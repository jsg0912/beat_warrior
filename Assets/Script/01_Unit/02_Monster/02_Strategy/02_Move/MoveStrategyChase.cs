using UnityEngine;

public class MoveStrategyChase : MoveStrategy
{
    private const float STOPOFFSET = 1f;
    private const float GROUNDCHECKRAY = 0.5f;

    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);

        moveSpeed = MonsterConstant.MoveSpeed[monster.monsterName];

        isEndOfGround = false;
    }

    public override bool PlayStrategy()
    {
        bool success = base.PlayStrategy();

        // [Code Review - KMJ] 위 base.PlayStrategy()가 false를 반환하면 아래 코드를 실행하지 않아야 하는지 확인 필요 - SDH, 20250116s
        CheckGround();
        ChaseTarget();
        return true;
    }

    protected override Vector3 GetRayStartPoint()
    {
        Vector3 offset = new Vector3(GetDirection(), 0, 0);
        return GetMonsterPos() + offset;
    }

    protected void CheckGround()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(GetRayStartPoint(), Vector3.down, GROUNDCHECKRAY, GroundLayer);

        if (rayHit.collider == null) isEndOfGround = true;
        else isEndOfGround = false;
    }

    protected Vector3 TargetPos()
    {
        return Player.Instance.transform.position;
    }

    protected void ChaseTarget()
    {
        if (TargetPos().x > GetMonsterPos().x) SetDirection(Direction.Right);
        else SetDirection(Direction.Left);
    }

    protected override bool IsMoveable()
    {
        if (isEndOfGround == true || Mathf.Abs(TargetPos().x - GetMonsterPos().x) < STOPOFFSET)
        {
            monster.SetIsWalking(false);
            return false;
        }

        return monster.GetIsMoveable();
    }
}
