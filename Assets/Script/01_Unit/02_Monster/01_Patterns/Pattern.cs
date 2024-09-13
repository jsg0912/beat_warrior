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

        Recognize?.Initialize(monster);
        MoveNormal?.Initialize(monster);
        MoveChase?.Initialize(monster);
        Attack?.Initialize(monster);
    }

    public virtual void PlayPattern()
    {
        Recognize?.PlayStrategy();
        if (monster.GetStatus() == MonsterStatus.Normal) MoveNormal?.PlayStrategy();
        if (monster.GetStatus() == MonsterStatus.Chase) MoveChase?.PlayStrategy();
        Attack?.PlayStrategy();
    }
}