using UnityEngine;

public class MoveStrategyRandom : MoveStrategy
{
    protected bool isStop = false;
    protected float changeDestTime = 0;

    public override void PlayStrategy()
    {
        base.PlayStrategy();
        ChangeDest();
    }

    protected override void Move()
    {
        if (isStop == true)
        {
            monster.SetIsWalking(false);
            return;
        }

        base.Move();
    }

    protected void ChangeDest()
    {
        if (changeDestTime > 0)
        {
            changeDestTime -= Time.deltaTime;
            return;
        }

        int dest = Random.Range(0, 3);
        switch (dest)
        {
            case 0:
                ChangeDirection();
                isStop = false;
                break;
            case 1:
                isStop = true;
                break;
            case 2:
                isStop = false;
                break;
        }

        changeDestTime = 3 + Random.value * 2;
    }
}
