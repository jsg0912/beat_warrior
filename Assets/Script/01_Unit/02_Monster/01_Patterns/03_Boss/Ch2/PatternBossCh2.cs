public class PatternCh2Boss : PatternBoss
{
    private AttackStrategyMelee attackStrategyRightHook;
    private AttackStrategyMelee attackStrategyLeftHook;
    private AttackStrategyMelee attackStrategyFullSwing;
    private AttackStrategyMelee attackStrategyRightTripleCombo;
    private AttackStrategyMelee attackStrategyLeftTripleCombo;
    private AttackStrategyThrow attackStrategyRange;
    private AttackStrategyThrow attackStrategySpawnIppali;
    private AttackStrategyMeleeRandomAttack attackStrategySummonTentacle;
    private AttackStrategyMeleeRandomAttack attackStrategyFullTentacle;

    private int attackCounter = 0;
    private Timer attackCoolTimer;

    public PatternCh2Boss() : base(PatternPhase.Phase2)
    {
        attackStrategyRightHook = new AttackStrategyMelee(BossConstantCh2.AttackAnimTriggerRightHook);
        attackStrategyLeftHook = new AttackStrategyMelee(BossConstantCh2.AttackAnimTriggerLeftHook);
        attackStrategyFullSwing = new AttackStrategyMelee(BossConstantCh2.AttackAnimTriggerFullSwing);
        attackStrategyLeftTripleCombo = new AttackStrategyMelee(BossConstantCh2.AttackAnimTriggerLeftTripleCombo);
        attackStrategyRightTripleCombo = new AttackStrategyMelee(BossConstantCh2.AttackAnimTriggerRightHook);
        // RangeAttack: AttackStrategyThrow
        // IppaliSpawn: AttackStrategyThrow
        // SummonTentacle: AttackStrategyCreate
        // FullTentacle: AttackStrategyCreate

        attackStrategies[PatternPhase.Phase1] = new()
        {
            attackStrategyRange,
            attackStrategySummonTentacle,
            attackStrategyFullTentacle
        };
        attackStrategies[PatternPhase.Phase2] = new()
        {
            attackStrategyRange,
            attackStrategySummonTentacle,
            attackStrategyFullTentacle,
            attackStrategyRightTripleCombo,
            attackStrategyLeftTripleCombo
        };

        attackCoolTimer = new Timer(BossConstantCh2.AttackCoolTime);
    }

    protected override void CheckPhase()
    {
        if (currentPhase == PatternPhase.Phase1)
        {
            if (monster.GetCurrentHP() <= monster.GetFinalStat(StatKind.HP) * BossConstantCh2.Phase2Threshold)
            {
                ChangePhase(PatternPhase.Phase2);
            }
        }
    }


    override public void PlayPattern()
    {
        base.PlayPattern();
        // 공격과정
        if (!attackCoolTimer.Tick() && monster.GetIsAttackAble())
        {
            if (attackCounter % BossConstantCh2.IppaliSpawnCycle == 0)
            {
                attackStrategySpawnIppali.PlayStrategy(ResetAttackCoolTimer);
            }
            else
            {
                RandomSystem.GetRandom(attackStrategies[currentPhase]).PlayStrategy(ResetAttackCoolTimer);
            }
            attackCounter++;
        }
    }

    private void ResetAttackCoolTimer() { attackCoolTimer.Initialize(); }
}