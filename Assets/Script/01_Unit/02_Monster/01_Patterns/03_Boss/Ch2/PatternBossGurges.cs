public class PatternBossGurges : PatternBoss
{
    private BossGergus bossGurges;
    private AttackStrategyMelee attackStrategyRightHook;
    private AttackStrategyMelee attackStrategyLeftHook;
    private AttackStrategyMelee attackStrategyFullSwing;
    private AttackStrategyMelee attackStrategyRightTripleCombo; // 촉수 3단 휩쓸기기
    private AttackStrategyMelee attackStrategyLeftTripleCombo; // 촉수 3단 휩쓸기기
    private AttackStrategyThrowMany attackStrategyRange; // 토사물 뱉기기
    private AttackStrategyThrowMany attackStrategyRangeMad; // 광폭화 토사물 뱉기기
    private AttackStrategyThrowManyIppali attackStrategySpawnIppali; // 이빨이 뱉기기
    private AttackStrategyMeleeRandomAttack attackStrategySummonTentacleHorizontal; // 촉수 수평 지르기
    private AttackStrategyMeleeRandomAttack attackStrategySummonTentacleVertical; // 촉수 수직 지르기
    private AttackStrategyMeleeRandomAttack attackStrategyFullTentacle; // 촉수 끈쩍이

    private int attackCounter = 0;
    private Timer attackCoolTimer;

    public PatternBossGurges() : base(PatternPhase.Phase2)
    {
    }

    override public void Initialize(Monster monster)
    {
        bossGurges = monster as BossGergus;

        attackStrategyRightHook = new AttackStrategyMelee(BossConstantCh2.AttackAnimTriggerRightHook);
        attackStrategyLeftHook = new AttackStrategyMelee(BossConstantCh2.AttackAnimTriggerLeftHook);
        attackStrategyFullSwing = new AttackStrategyMelee(BossConstantCh2.AttackAnimTriggerFullSwing);

        attackStrategyLeftTripleCombo = new AttackStrategyMelee(BossConstantCh2.AttackAnimTriggerLeftTripleCombo);
        attackStrategyRightTripleCombo = new AttackStrategyMelee(BossConstantCh2.AttackAnimTriggerRightTripleCombo);

        attackStrategyRange = new AttackStrategyThrowMany(BossConstantCh2.DisgorgeSpeed, BossConstantCh2.DisgorgeMaxHeight, BossConstantCh2.EnergyBallSpawnNumber, BossConstantCh2.SpawnInterval, PoolTag.GurgesThrow, BossConstantCh2.AttackAnimTriggerIppaliSpawn);
        attackStrategyRangeMad = new AttackStrategyThrowMany(BossConstantCh2.DisgorgeSpeed, BossConstantCh2.DisgorgeMaxHeight, BossConstantCh2.EnergyBallSpawnNumberMad, BossConstantCh2.SpawnInterval, PoolTag.GurgesThrow, BossConstantCh2.AttackAnimTriggerIppaliSpawn);

        attackStrategySpawnIppali = new AttackStrategyThrowManyIppali(BossConstantCh2.DisgorgeSpeed, BossConstantCh2.DisgorgeMaxHeight, BossConstantCh2.IppaliSpawnNumber, BossConstantCh2.SpawnInterval, PoolTag.IppaliEgg, BossConstantCh2.AttackAnimTriggerIppaliSpawn);

        attackStrategySummonTentacleHorizontal = new AttackStrategyMeleeRandomAttack(BossConstantCh2.AttackAnimTriggerSummonTentacle, bossGurges.tentaclesHorizontal);
        attackStrategySummonTentacleVertical = new AttackStrategyMeleeRandomAttack(BossConstantCh2.AttackAnimTriggerSummonTentacle, bossGurges.tentaclesVertical);

        attackStrategyFullTentacle = new AttackStrategyMeleeRandomAttack(BossConstantCh2.AttackAnimTriggerFullTentacle, bossGurges.tentaclesCrash);

        attackStrategies[PatternPhase.Phase1] = new()
        {
            attackStrategyRange,
            attackStrategySummonTentacleHorizontal,
            attackStrategySummonTentacleVertical,
            attackStrategyFullTentacle
        };
        attackStrategies[PatternPhase.Phase2] = new()
        {
            attackStrategyRangeMad,
            attackStrategySummonTentacleHorizontal,
            attackStrategySummonTentacleVertical,
            attackStrategyFullTentacle,
            attackStrategyRightTripleCombo,
            attackStrategyLeftTripleCombo
        };

        attackCoolTimer = new Timer(BossConstantCh2.AttackCoolTime);
        attackCoolTimer.SetRemainTimeZero();
        attackStrategySpawnIppali.Initialize(bossGurges);
        base.Initialize(bossGurges);
    }

    protected override void CheckPhase()
    {
        if (currentPhase == PatternPhase.Phase1)
        {
            if (monster.GetCurrentHP() <= monster.GetFinalStat(StatKind.HP) * BossConstantCh2.Phase2Threshold)
            {
                ChangePhase(PatternPhase.Phase2);
                bossGurges.PlayAnimation(BossConstantCh2.StandAnimTrigger);
            }
        }
    }

    override public void PlayPattern()
    {
        if (SystemMessageUIManager.Instance.isTimeLinePlaying) return;
        base.PlayPattern();
        if (!monster.GetIsAttacking() && !attackCoolTimer.Tick() && monster.GetIsAttackAble())
        {
            if (attackCounter % BossConstantCh2.IppaliSpawnCycle == 0)
            {
                currentAttackStrategy = attackStrategySpawnIppali;
                attackStrategySpawnIppali.PlayStrategy();
                bossGurges.SetStatus(MonsterStatus.Attack);
            }
            else
            {
                currentAttackStrategy = RandomSystem.GetRandom(attackStrategies[currentPhase]);
                currentAttackStrategy.PlayStrategy();
                bossGurges.SetStatus(MonsterStatus.Attack);
            }
            attackCounter++;
        }
    }

    public void ResetAttackCoolTimer()
    {
        attackCoolTimer.Initialize();
    }
}