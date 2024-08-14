public class PassiveSkillBuff : PassiveSkill
{
    protected StatKind statKind;
    protected int statBuff;

    public override void GetPassiveSkill()
    {
        Player.Instance.playerUnit.unitStat.SetBuffPlus(statKind, statBuff);
    }

    public override void RemovePassiveSkill()
    {
        Player.Instance.playerUnit.unitStat.ResetBuffPlus(statKind);
    }
}
