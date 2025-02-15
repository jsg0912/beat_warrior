public class PatternCh2Boss : PatternBoss
{
    private AttackStrategyMelee attackStrategyRightHook;
    private AttackStrategyMelee attackStrategyLeftHook;
    private AttackStrategyMelee attackStrategyFullSwing;
    private AttackStrategyMelee attackStrategyRightTripleCombo;
    private AttackStrategyMelee attackStrategyLeftTripleCombo;
    private AttackStrategyThrow attackStrategyRange;
    private AttackStrategyThrow attackStrategyIppaliSpawn;
    private AttackStrategyCreate attackStrategySummonTentacle;
    private AttackStrategyCreate attackStrategyFullTentacle;

    private int attackCounter = 0;

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
            attackStrategyIppaliSpawn,
            attackStrategyRange,
            attackStrategySummonTentacle,
            attackStrategyFullTentacle
        };
        attackStrategies[PatternPhase.Phase2] = new()
        {
            attackStrategyIppaliSpawn,
            attackStrategyRange,
            attackStrategySummonTentacle,
            attackStrategyFullTentacle,
            attackStrategyRightTripleCombo,
            attackStrategyLeftTripleCombo
        };
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
}