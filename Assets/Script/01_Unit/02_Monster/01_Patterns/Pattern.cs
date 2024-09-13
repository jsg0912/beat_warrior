public class Pattern
{
    protected Monster monster;

    protected RecognizeStrategy Recognize;
    protected MoveStrategy MoveNormal;
    protected MoveStrategy MoveChase;
    protected AttackStrategy Attack;

    public virtual void Initialize(Monster monster)
    {
        Recognize?.Initialize(monster);
        MoveNormal?.Initialize(monster);
        MoveChase?.Initialize(monster);
        Attack?.Initialize(monster);
    }

    public virtual void PlayPattern()
    {
        Recognize?.PlayStrategy();
        MoveNormal?.PlayStrategy();
        MoveChase?.PlayStrategy();
        Attack?.PlayStrategy();
    }
}