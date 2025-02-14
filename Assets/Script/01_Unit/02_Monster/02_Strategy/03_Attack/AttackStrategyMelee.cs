public class AttackStrategyMelee : AttackStrategy
{
    protected override void AttackMethod()
    {
        monster.ForceIsTackleAble(true);
    }

    public override void AttackEnd()
    {
        base.AttackEnd();
        monster.SetIsTackleAble(false);
    }
}