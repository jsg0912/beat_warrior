public class AttackStrategyMelee : AttackStrategy
{
    public AttackStrategyMelee(string monsterAnimTrigger = MonsterAnimTrigger.attackChargeAnimTrigger) : base(monsterAnimTrigger)
    {

    }

    // 공격은 Animation에서 Collider를 끄면서 공격함
    protected override void AttackMethod()
    {
    }
}