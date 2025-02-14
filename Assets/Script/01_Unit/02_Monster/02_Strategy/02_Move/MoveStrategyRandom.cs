using UnityEngine;

public class MoveStrategyRandom : MoveStrategy
{
    protected bool isStop = false;

    public override bool PlayStrategy()
    {
        base.PlayStrategy();
        TryFlipDirection();
        return true;
    }

    protected override bool TryMove()
    {
        if (isStop == true)
        {
            monster.SetWalkingAnimation(false);
            return false;
        }

        return base.TryMove();
    }

    protected override void TryFlipDirection()
    {
        if (changeDestTimer.Tick()) return;

        // TODO: [Code Review - KMJ] Constant화 해야함
        int dest = Random.Range(0, 3);
        switch (dest)
        {
            case 0:
                FlipDirection();
                isStop = false;
                break;
            case 1:
                isStop = true;
                break;
            case 2:
                isStop = false;
                break;
        }

        SetRandomFlipTimer();
    }
}
