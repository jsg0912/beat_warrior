public class AttackStrategyMelee : AttackStrategy
{
    protected override void SkillMethod()
    {
        Util.SetActive(monster.attackCollider, true);
        monster.SetIsTackleAble(true);
    }

    public override void AttackEnd()
    {
        Util.SetActive(monster.attackCollider, false);
        monster.SetIsTackleAble(false);
    }
}