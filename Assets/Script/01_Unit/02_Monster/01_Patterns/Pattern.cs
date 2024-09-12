using UnityEngine;

public class Pattern
{
    protected Monster monster;

    protected RecognizeStrategy Recognize;
    protected MoveStrategy Move;
    protected AttackStrategy Attack;

    public virtual void Initialize(Monster monster)
    {
        if (Recognize != null) Recognize.Initialize(monster);
        if (Move != null) Move.Initialize(monster);
        if (Attack != null) Attack.Initialize(monster);
    }

    public virtual void PlayPattern()
    {
        if (Recognize != null) Recognize.PlayStrategy();
        if (Move != null) Move.PlayStrategy();
        if (Attack != null) Attack.PlayStrategy();
    }
}