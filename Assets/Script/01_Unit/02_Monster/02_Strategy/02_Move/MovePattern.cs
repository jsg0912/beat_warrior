using UnityEngine;

public class MoveStrategy : Strategy
{
    protected float moveSpeed;

    protected LayerMask GroundLayer;
    protected bool isEndOfGround;

    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);

        moveSpeed = MonsterConstant.MoveSpeed[monster.monsterName];

        GroundLayer = LayerMask.GetMask(MonsterConstant.GroundLayer);
        isEndOfGround = false;

        // 초기 방향 랜덤 설정
        monster.SetDirection(Random.Range(0, 1) == 0 ? Direction.Right : Direction.Left);
    }

    public override void PlayStrategy()
    {
        Move();
        CheckGround();
        DecideDirection();
    }

    protected virtual void Move()
    {
        if (IsMoveable() == false) return;

        monster.gameObject.transform.position += new Vector3(direction() * moveSpeed * Time.deltaTime, 0, 0);
        monster.SetAnimation();
    }

    protected virtual void CheckGround()
    {
        Vector3 offset = new Vector3(monster.GetDirection(), 0, 0);
        RaycastHit2D rayHit = Physics2D.Raycast(CurrentPos() + offset, Vector3.down, 0.5f, GroundLayer);

        if (rayHit.collider == null) isEndOfGround = true;
        else isEndOfGround = false;
    }

    protected void DecideDirection()
    {
        if (isEndOfGround == false) return;

        if (monster.GetMonsterStatus() == MonsterStatus.Chase) monster.SetDirection(Direction.Stop);
        else ChangeDirection();
    }

    protected virtual bool IsMoveable() { return isEndOfGround == false; }
    protected int direction() { return monster.GetDirection(); }
    protected void SetDirection(Direction direction) { monster.SetDirection(direction); }
    protected void ChangeDirection() { monster.ChangeDirection(); }
}
