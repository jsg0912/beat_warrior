public class Pattern
{
    protected Monster monster;

    protected RecognizeStrategy Recognize;
    protected MoveStrategy MoveNormal;
    protected MoveStrategy MoveChase;
    protected AttackStrategy Attack;

    public virtual void Initialize(Monster monster)
    {
        if (Recognize != null) Recognize.Initialize(monster);
        if (MoveNormal != null) MoveNormal.Initialize(monster);
        if (MoveChase != null) MoveChase.Initialize(monster);
        if (Attack != null) Attack.Initialize(monster);
    }

    public virtual void PlayPattern()
    {
        if (Recognize != null) Recognize.PlayStrategy();
        if (MoveNormal != null && monster.GetStatus() == MonsterStatus.Normal) MoveNormal.PlayStrategy();
        if (MoveChase != null && monster.GetStatus() == MonsterStatus.Chase) MoveChase.PlayStrategy();
        if (Attack != null) Attack.PlayStrategy();
    }
}