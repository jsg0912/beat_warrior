using UnityEngine;

public abstract class Pattern
{
    protected GameObject gameObject;
    protected Monster monster;

    protected float moveSpeed;

    public virtual void Initialize(GameObject gameObject)
    {
        this.gameObject = gameObject;
        monster = gameObject.GetComponent<Monster>();

        moveSpeed = MonsterConstant.MoveSpeed[monster.monsterName];

        // 초기 방향 랜덤 설정
        monster.SetDirection(Random.Range(0, 1) == 0 ? Direction.Right : Direction.Left);
    }

    protected void Move()
    {
        if (IsMoveable() == false) return;

        gameObject.transform.position += new Vector3(direction() * moveSpeed * Time.deltaTime, 0, 0);
    }

    // Move 관련 함수
    protected virtual bool IsMoveable() { return false; }
    protected int direction() { return monster.GetDirection(); }
    protected void SetDirection(Direction direction) { monster.SetDirection(direction); }
    protected void ChangeDirection() { monster.ChangeDirection(); }

    public virtual void PlayPattern() { }

    public virtual Pattern Copy() { return this; }
}