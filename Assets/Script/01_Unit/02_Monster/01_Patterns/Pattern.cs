public class Pattern
{
    protected Monster monster;

    protected RecognizeStrategy Recognize;
    protected MoveStrategy MoveNormal;
    protected MoveStrategy MoveChase;
    protected AttackStrategy Attack;

    public virtual void Initialize(Monster monster)
    {
        this.monster = monster;

        if (Recognize != null) Recognize.Initialize(monster);
        if (MoveNormal != null) MoveNormal.Initialize(monster);
        if (MoveChase != null) MoveChase.Initialize(monster);
        if (Attack != null) Attack.Initialize(monster);
    }

    public virtual void PlayPattern()
    {
        Recognize?.PlayStrategy();
        MoveNormal?.PlayStrategy();
        MoveChase?.PlayStrategy();
        Attack?.PlayStrategy();
    }
}