public class PatternCh2Boss : PatternBoss
{
    private BossGergus bossGergus;
    private AttackStrategyMelee attackStrategyRightHook;
    private AttackStrategyMelee attackStrategyLeftHook;
    private AttackStrategyMelee attackStrategyFullSwing;
    private AttackStrategyMelee attackStrategyRightTripleCombo; // 촉수 3단 휩쓸기기
    private AttackStrategyMelee attackStrategyLeftTripleCombo; // 촉수 3단 휩쓸기기
    private AttackStrategyThrowMany attackStrategyRange; // 토사물 뱉기기
    private AttackStrategyThrowMany attackStrategyRangeMad; // 광폭화 토사물 뱉기기
    private AttackStrategyThrowManyIppali attackStrategySpawnIppali; // 이빨이 뱉기기
    private AttackStrategyMeleeRandomAttack attackStrategySummonTentacleHorizontal; // 촉수 지르기
    private AttackStrategyMeleeRandomAttack attackStrategySummonTentacleVertical; // 촉수 지르기
    private AttackStrategyMeleeRandomAttack attackStrategyFullTentacle;

    private int attackCounter = 0;
    private Timer attackCoolTimer;

    public PatternCh2Boss() : base(PatternPhase.Phase2)
    {
    }

    override public void Initialize(Monster monster)
    {
        DebugConsole.Log("PatternCh2Boss Initialize");
        bossGergus = monster as BossGergus;

        attackStrategyRightHook = new AttackStrategyMelee(BossConstantCh2.AttackAnimTriggerRightHook);
        attackStrategyLeftHook = new AttackStrategyMelee(BossConstantCh2.AttackAnimTriggerLeftHook);
        attackStrategyFullSwing = new AttackStrategyMelee(BossConstantCh2.AttackAnimTriggerFullSwing);

        attackStrategyLeftTripleCombo = new AttackStrategyMelee(BossConstantCh2.AttackAnimTriggerLeftTripleCombo);
        attackStrategyRightTripleCombo = new AttackStrategyMelee(BossConstantCh2.AttackAnimTriggerRightHook);

        attackStrategyRange = new AttackStrategyThrowMany(BossConstantCh2.DisgorgeSpeed, BossConstantCh2.DisgorgeMaxHeight, BossConstantCh2.EnergyBallSpawnNumber, BossConstantCh2.SpawnInterval, PoolTag.GergusThrow, BossConstantCh2.AttackAnimTriggerIppaliSpawn);
        attackStrategyRangeMad = new AttackStrategyThrowMany(BossConstantCh2.DisgorgeSpeed, BossConstantCh2.DisgorgeMaxHeight, BossConstantCh2.EnergyBallSpawnNumberMad, BossConstantCh2.SpawnInterval, PoolTag.GergusThrow, BossConstantCh2.AttackAnimTriggerIppaliSpawn);

        attackStrategySpawnIppali = new AttackStrategyThrowManyIppali(BossConstantCh2.DisgorgeSpeed, BossConstantCh2.DisgorgeMaxHeight, BossConstantCh2.IppaliSpawnNumber, BossConstantCh2.SpawnInterval, PoolTag.IppaliEgg, BossConstantCh2.AttackAnimTriggerIppaliSpawn);

        attackStrategySummonTentacleHorizontal = new AttackStrategyMeleeRandomAttack(BossConstantCh2.AttackAnimTriggerSummonTentacle, bossGergus.tentaclesHorizontal);
        attackStrategySummonTentacleVertical = new AttackStrategyMeleeRandomAttack(BossConstantCh2.AttackAnimTriggerSummonTentacle, bossGergus.tentaclesVertical);

        attackStrategyFullTentacle = new AttackStrategyMeleeRandomAttack(BossConstantCh2.AttackAnimTriggerFullTentacle, bossGergus.tentaclesCrash);

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
        attackStrategySpawnIppali.Initialize(bossGergus);
        base.Initialize(bossGergus);
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
        if (SystemMessageUIManager.Instance.isTimeLinePlaying) return;
        base.PlayPattern();
        if (!monster.GetIsAttacking() && !attackCoolTimer.Tick() && monster.GetIsAttackAble())
        {
            DebugConsole.Log("i can attack 씨발아");
            if (attackCounter % BossConstantCh2.IppaliSpawnCycle == 0)
            {
                DebugConsole.Log($"boss Attack ippali");
                currentAttackStrategy = attackStrategySpawnIppali;
                attackStrategySpawnIppali.PlayStrategy();
                bossGergus.SetStatus(MonsterStatus.Attack);
            }
            else
            {
                DebugConsole.Log($"boss Attack random");
                currentAttackStrategy = RandomSystem.GetRandom(attackStrategies[currentPhase]);
                currentAttackStrategy.PlayStrategy();
                bossGergus.SetStatus(MonsterStatus.Attack);
            }
            attackCounter++;
            DebugConsole.Log($"boss Attack {attackCounter}");
        }
    }

    public void ResetAttackCoolTimer()
    {
        DebugConsole.Log("Reset CoolTiem");
        attackCoolTimer.Initialize();
    }
}