using UnityEngine;

public class MoveStrategy : Strategy
{
    protected float moveSpeed;

    protected float timeLimit => 3 + Random.value * 2; // TODO: [Code Review - KMJ] Constant화 해야함
    protected Timer changeDestTimer = new Timer();

    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);
        SetRandomFlipTimer();
    }

    public override bool PlayStrategy()
    {
        return TryMove();
    }

    protected virtual bool TryMove()
    {
        if (!IsMoveable())
        {
            monster.SetWalkingAnimation(false);
            return false;
        }

        if ((CheckWall() || CheckEndOfGround()) && CheckGround()) FlipDirection();

        MoveFor(GetMovingDirection(), moveSpeed);
        monster.SetWalkingAnimation(true);
        return true;
    }

    protected virtual bool IsMoveable() { return monster.GetIsMoveable(); }
    protected void SetMovingDirection(Direction direction) { monster.SetMovingDirection(direction); }
    protected void FlipDirection() { monster.FlipDirection(); }
    protected virtual Vector3 GetRayStartPoint() { return GetMonsterPos(); }

    protected void SetRandomFlipTimer() { changeDestTimer.Initialize(timeLimit); }

    protected virtual void TryFlipDirection()
    {
        if (changeDestTimer.Tick()) return;

        // TODO: [Code Review - KMJ] Constant화 해야함
        int dest = Random.Range(0, 3);
        if (dest == 0) FlipDirection();
        SetRandomFlipTimer();
    }
}
