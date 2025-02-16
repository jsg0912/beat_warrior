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
        if (SystemMessageUIManager.Instance.isTimeLinePlaying) return;
        // 필수 실행
        Attack?.UpdateCoolTime();
        Recognize?.PlayStrategy();

        // Monster 상태에 따른 실행
        if (monster.GetIsRecognizing())
        {
            // bool의 부정이 반드시 들어가야함(Attack이 null일 수도 있으니)
            if (Attack?.PlayStrategy() != true) MoveChase?.PlayStrategy();
        }
        else MoveBasic?.PlayStrategy();
    }

    public void AttackStartMethod() { Attack?.AttackStart(); }
    public void AttackUpdateMethod() { Attack?.AttackUpdate(); }
    public void AttackEndMethod() { Attack?.AttackEnd(); }
    public void StopAttack() { Attack?.StopAttack(); }
    public void PlayGroggy() { Groggy?.PlayStrategy(); }
}