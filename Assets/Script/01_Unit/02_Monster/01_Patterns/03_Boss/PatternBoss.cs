using System;
using System.Collections.Generic;

public abstract class PatternBoss : Pattern
{
    protected Dictionary<PatternPhase, List<AttackStrategy>> attackStrategies = new();
    protected PatternPhase currentPhase = 0;
    protected PatternPhase maxPhase;
    protected AttackStrategy currentAttackStrategy;

    public PatternBoss(PatternPhase maxPhase)
    {
        Recognize = new RecognizeStrategyAbsolute();
        this.maxPhase = maxPhase;
    }

    abstract protected void CheckPhase();
    protected void ChangePhase(PatternPhase phase)
    {
        currentPhase = phase;
        monster.SetStatus(MonsterStatus.PhaseChanging);
    }

    override public void Initialize(Monster monster)
    {
        base.Initialize(monster);
        foreach (PatternPhase patternPhase in Enum.GetValues(typeof(PatternPhase)))
        {
            if (patternPhase > maxPhase) break;
            attackStrategies[patternPhase].ForEach(attackStrategy => attackStrategy.Initialize(monster));
        }
    }

    override public void PlayPattern()
    {
        CheckPhase();
        Recognize?.PlayStrategy();
        // Implement AttackPattern in each Boss
    }


    public override void AttackStartMethod() { currentAttackStrategy?.AttackStart(); }
}