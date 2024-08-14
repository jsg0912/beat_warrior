public class DoubleJump : PassiveSkill
{
    public override void Initialize()
    {
        GetPassiveSkill();
    }

    protected override void GetPassiveSkill()
    {
        Player.Instance.playerUnit.unitStat.SetBuffPlus(StatKind.JUMPCOUNT, 1);
    }

    protected override void RemovePassiveSkill()
    {
        Player.Instance.playerUnit.unitStat.ResetBuffPlus(StatKind.JUMPCOUNT);
    }
}
