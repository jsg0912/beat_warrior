public class AppendMaxHP : PassiveSkillBuffPlayer
{
    public override void GetSkill()
    {
        skillName = SkillName.AppendMaxHP;

        statKind = StatKind.HP;
        statBuff = 1;

        UIManager.Instance.AddHPUI();

        base.GetSkill();
    }

    public override void RemoveSkill()
    {
        UIManager.Instance.RemoveHPUI();

        base.RemoveSkill();
    }
}
