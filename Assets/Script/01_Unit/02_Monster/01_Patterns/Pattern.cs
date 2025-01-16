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

    // It called at every Update.
    public virtual void PlayPattern()
    {
        Recognize?.PlayStrategy();
        Attack?.UpdateCoolTime();

        switch (monster.GetStatus())
        {
            case MonsterStatus.Normal:
                MoveNormal?.PlayStrategy();
                break;
            case MonsterStatus.Chase:
                if (Attack?.PlayStrategy() == false)
                {
                    MoveChase?.PlayStrategy();
                }
                break;
        }
    }

    public void StopAttack() { Attack?.StopAttack(); }
}