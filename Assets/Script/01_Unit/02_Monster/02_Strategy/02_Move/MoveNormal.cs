using UnityEngine;

public class MoveNormal : MoveStrategy
{
    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);

        moveSpeed = MonsterConstant.MoveSpeed[monster.monsterName];

        GroundLayer = LayerMask.GetMask(MonsterConstant.GroundLayer);
        isEndOfGround = false;

        // 초기 방향 랜덤 설정
        SetDirection(Random.Range(0, 2) == 0 ? Direction.Right : Direction.Left);
    }

    public override void PlayStrategy()
    {
        Move();
        CheckGround();
    }

    protected virtual void Move()
    {
        if (IsMoveable() == false) return;

        monster.gameObject.transform.position += new Vector3(direction() * moveSpeed * Time.deltaTime, 0, 0);
        monster.IsWalking(true);
    }

    protected virtual void CheckGround()
    {
        Vector3 offset = new Vector3(monster.GetDirection(), 0, 0);
        RaycastHit2D rayHit = Physics2D.Raycast(CurrentPos() + offset, Vector3.down, 0.5f, GroundLayer);

        if (rayHit.collider == null) ChangeDirection();
    }
}
