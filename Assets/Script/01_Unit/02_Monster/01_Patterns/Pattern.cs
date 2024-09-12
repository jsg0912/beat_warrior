using UnityEngine;

public class Pattern
{
    protected GameObject gameObject;
    protected Monster monster;

    protected RecognizeStrategy Recognize;
    protected MoveStrategy Move;
    protected AttackStrategy Attack;

    public virtual void Initialize(GameObject gameObject)
    {
        this.gameObject = gameObject;
        Recognize.Initialize(gameObject);
        Move.Initialize(gameObject);
        Attack.Initialize(gameObject);
    }

    public void SetMonster(Monster monster)
    {
        this.monster = monster;
    }

    public void Play()
    {
        Recognize.PlayStrategy();
        Move.PlayStrategy();
        Attack.PlayStrategy();
    }

    public virtual void PlayPattern()
    {

    }
}