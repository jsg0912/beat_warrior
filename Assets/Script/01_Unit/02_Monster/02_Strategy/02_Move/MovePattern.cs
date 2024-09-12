using UnityEngine;

public class MoveStrategy : Strategy
{
    protected float moveSpeed;

    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);

        moveSpeed = MonsterConstant.MoveSpeed[monster.monsterName];

        // 초기 방향 랜덤 설정
        monster.SetDirection(Random.Range(0, 1) == 0 ? Direction.Right : Direction.Left);
    }

    public override void PlayStrategy()
    {
        Move();
    }

    protected virtual void Move()
    {
        if (IsMoveable() == false) return;

        monster.gameObject.transform.position += new Vector3(direction() * moveSpeed * Time.deltaTime, 0, 0);
    }

    protected virtual bool IsMoveable() { return false; }
    protected int direction() { return monster.GetDirection(); }
    protected void SetDirection(Direction direction) { monster.SetDirection(direction); }
    protected void ChangeDirection() { monster.ChangeDirection(); }
}
