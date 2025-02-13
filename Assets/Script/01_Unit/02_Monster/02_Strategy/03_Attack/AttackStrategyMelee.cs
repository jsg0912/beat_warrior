public class AttackStrategyMelee : AttackStrategy
{
    protected override void SkillMethod()
    {
        monster.SetIsTackleAble(true);
    }

    public override void AttackEnd()
    {
        base.AttackEnd();

        monster.SetIsTackleAble(false);
    }
}