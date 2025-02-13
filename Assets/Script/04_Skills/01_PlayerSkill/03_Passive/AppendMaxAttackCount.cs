using UnityEngine;

public class AppendMaxAttackCount : PassiveSkillBuffPlayer
{
    public AppendMaxAttackCount(GameObject unit) : base(unit)
    {
        statKind = StatKind.AttackCount;
        statBuff = 1;
    }

    protected override void SetSkillName() { skillName = SkillName.AppendAttack; }

    public override void GetSkill()
    {
        base.GetSkill();
        AttackCountUI.Instance.UpdateUI();
    }

    public override void RemoveSkill()
    {
        base.RemoveSkill();
        AttackCountUI.Instance.UpdateUI();
    }
}