using UnityEngine;

public class MoveStrategyNormal : MoveStrategyRandom
{
    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);

        moveSpeed = MonsterConstant.MoveSpeed[monster.monsterName];

        // 초기 방향 랜덤 설정
        SetMovingDirection(Random.Range(0, 2) == 0 ? Direction.Right : Direction.Left);
    }

    public override bool PlayStrategy()
    {
        base.PlayStrategy();

        CheckGround();
        return true;
    }

    protected override Vector3 GetRayStartPoint()
    {
        Vector3 bottomPos = GetMonsterBottomPos();
        return bottomPos + new Vector3(GetMonsterSize().x * GetMovingDirectionFloat() / 2, 0, 0);
    }

    protected void CheckGround()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(GetRayStartPoint(), Vector3.down, 0.1f, GroundLayer);
        //Debug.DrawLine(GetRayStartPoint(), GetMonsterPos() + offset + Vector3.down * 0.1f, Color.red);

        if (rayHit.collider == null) FlipDirection();
    }
}
