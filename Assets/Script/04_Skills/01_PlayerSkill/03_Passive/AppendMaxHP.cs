using UnityEngine;

public class AppendMaxHP : PassiveSkillBuffPlayer
{
    public AppendMaxHP(GameObject unit) : base(unit)
    {
        statKind = StatKind.HP;
        statBuff = 1;
    }

    protected override void SetSkillName() { skillName = SkillName.AppendMaxHP; }

    public override void GetSkill()
    {
        base.GetSkill();
        PlayerHpUIController.InstanceWithoutCreate?.CreateAndUpdateHPUI();
    }

    public override void RemoveSkill()
    {
        base.RemoveSkill();
        PlayerHpUIController.InstanceWithoutCreate?.CreateAndUpdateHPUI();
    }
}