using System.Text;
using UnityEngine;

public class AppendMaxHP : PassiveSkillBuffPlayer
{
    public AppendMaxHP(GameObject unit) : base(unit)
    {
        skillName = SkillName.AppendMaxHP;

        statKind = StatKind.HP;
        statBuff = 1;
    }

    public override void GetSkill()
    {
        base.GetSkill();
        HpUI.Instance.CreateAndUpdateHPUI(Player.Instance.GetFinalStat(StatKind.HP));
    }

    public override void RemoveSkill()
    {
        base.RemoveSkill();
        HpUI.Instance.CreateAndUpdateHPUI(Player.Instance.GetFinalStat(StatKind.HP));
    }
}
