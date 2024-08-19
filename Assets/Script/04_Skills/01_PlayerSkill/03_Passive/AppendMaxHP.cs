using System.Text;
using UnityEngine;

public class AppendMaxHP : PassiveSkillBuffPlayer
{
    public AppendMaxHP(GameObject unit) : base(unit) { }

    public override void GetSkill()
    {
        skillName = SkillName.AppendMaxHP;

        statKind = StatKind.HP;
        statBuff = 1;

        base.GetSkill();
    }
}
