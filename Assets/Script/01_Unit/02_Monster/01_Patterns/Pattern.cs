public abstract class Pattern
{
    protected Monster monster;

    protected RecognizeStrategy Recognize;
    protected MoveStrategy MoveBasic;
    protected MoveStrategy MoveChase;
    protected AttackStrategy Attack;

    public virtual void Initialize(Monster monster)
    {
        this.monster = monster;

        Recognize?.Initialize(monster);
        MoveBasic?.Initialize(monster);
        MoveChase?.Initialize(monster);
        Attack?.Initialize(monster);
    }

    // It called at every Update.
    public virtual void PlayPattern()
    {
        Attack?.UpdateCoolTime();

        switch (monster.GetStatus())
        {
            case MonsterStatus.Idle:
                Recognize?.PlayStrategy();
                MoveBasic?.PlayStrategy();
                break;
            case MonsterStatus.Chase:
                Recognize?.PlayStrategy();
                if (Attack?.PlayStrategy() == false)
                {
                    MoveChase?.PlayStrategy();
                }
                break;
        }
    }

    public void StopAttack() { Attack?.StopAttack(); }
}