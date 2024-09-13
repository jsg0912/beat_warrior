using UnityEngine;

public class MoveChase : MoveStrategy
{
    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);

        moveSpeed = MonsterConstant.MoveSpeed[monster.monsterName];

        isEndOfGround = false;
    }

    public override void PlayStrategy()
    {
        base.PlayStrategy();

        CheckGround();
        ChaseTarget();
    }

    protected override void Move()
    {
        if (IsMoveable() == false) return;

        monster.gameObject.transform.position += new Vector3(direction() * moveSpeed * Time.deltaTime, 0, 0);
        monster.IsWalking(true);
    }

    protected void CheckGround()
    {
        Vector3 offset = new Vector3(monster.GetDirection(), 0, 0);
        RaycastHit2D rayHit = Physics2D.Raycast(CurrentPos() + offset, Vector3.down, 0.5f, GroundLayer);

        if (rayHit.collider == null) isEndOfGround = true;
        else isEndOfGround = false;
    }

    protected Vector3 TargetPos()
    {
        return Player.Instance.transform.position;
    }

    protected void ChaseTarget()
    {
        if (TargetPos().x > CurrentPos().x) monster.SetDirection(Direction.Right);
        else monster.SetDirection(Direction.Left);
    }

    protected override bool IsMoveable()
    {
        if (isEndOfGround == true || Mathf.Abs(TargetPos().x - CurrentPos().x) < 0.1f)
        {
            monster.IsWalking(false);
            return false;
        }
        return true;
    }
}
