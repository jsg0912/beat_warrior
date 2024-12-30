using UnityEngine;

public class MoveStrategyNormal : MoveStrategy
{
    private float colliderSizeX;
    private float colliderSizeY;

    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);

        moveSpeed = MonsterConstant.MoveSpeed[monster.monsterName];

        BoxCollider2D bc = monster.gameObject.GetComponent<BoxCollider2D>();
        colliderSizeX = bc.size.x / 2;
        colliderSizeY = bc.size.y / 2 - bc.offset.y;

        // 초기 방향 랜덤 설정
        SetDirection(Random.Range(0, 2) == 0 ? Direction.Right : Direction.Left);
    }

    public override void PlayStrategy()
    {
        base.PlayStrategy();

        CheckGround();
    }

    protected void CheckGround()
    {
        Vector3 offset = new Vector3(GetDirection() * colliderSizeX, -colliderSizeY, 0);
        RaycastHit2D rayHit = Physics2D.Raycast(GetMonsterPos() + offset, Vector3.down, 0.1f, GroundLayer);
        //Debug.DrawLine(GetMonsterPos() + offset, GetMonsterPos() + offset + Vector3.down * 0.1f, Color.red);

        if (rayHit.collider == null) ChangeDirection();
    }
}
