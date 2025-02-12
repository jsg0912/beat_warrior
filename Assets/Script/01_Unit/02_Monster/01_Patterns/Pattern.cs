public abstract class Pattern
{
    protected Monster monster;

    protected RecognizeStrategy Recognize;
    protected MoveStrategy MoveBasic;
    protected MoveStrategy MoveChase;
    protected AttackStrategy Attack;
    protected GroggyStrategy Groggy;

    public virtual void Initialize(Monster monster)
    {
        this.monster = monster;

        Recognize?.Initialize(monster);
        MoveBasic?.Initialize(monster);
        MoveChase?.Initialize(monster);
        Attack?.Initialize(monster);
        Groggy?.Initialize(monster);
    }

    // It called at every Update.
    public virtual void PlayPattern()
    {
        Attack?.UpdateCoolTime();

        if (!monster.isChasing)
        {
            Recognize?.PlayStrategy();
            MoveBasic?.PlayStrategy();
            return;
        }

        Recognize?.PlayStrategy();
        if (Attack?.PlayStrategy() == false)
        {
            MoveChase?.PlayStrategy();
        }
    }

    public void AttackStartMethod() { Attack?.AttackStart(); }
    public void AttackOnMethod() { Attack?.AttackOn(); }
    public void AttackEndMethod() { Attack?.AttackEnd(); }
    public void StopAttack() { Attack?.StopAttack(); }
}