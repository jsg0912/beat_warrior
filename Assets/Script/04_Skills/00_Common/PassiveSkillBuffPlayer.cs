using UnityEngine;

public class PassiveSkillBuffPlayer : PassiveSkill
{
    protected StatKind statKind;
    protected int statBuff;

    public PassiveSkillBuffPlayer(GameObject unit) : base(unit) { }

    public override void GetSkill()
    {
        Player.Instance.playerUnit.unitStat.SetBuffPlus(statKind, statBuff);
    }

    public override void RemoveSkill()
    {
        Player.Instance.playerUnit.unitStat.ResetBuffPlus(statKind);
    }
}
