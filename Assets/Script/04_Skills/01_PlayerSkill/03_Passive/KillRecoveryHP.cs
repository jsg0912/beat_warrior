using UnityEngine;

public class KillRecoveryHP : PassiveSkill
{
    private int recoveryHP;

    private int killMonsterCount;
    private int killMonsterCountMax;

    public KillRecoveryHP(GameObject unit) : base(unit, SkillTier.Epic)
    {
        skillName = SkillName.KillRecoveryHP;

        recoveryHP = 1;

        killMonsterCount = 0;
        killMonsterCountMax = PlayerSkillConstant.KillRecoveryHPTrigger;
    }

    public override void GetSkill()
    {
        Player.Instance.hitMonsterFuncList += CountKillMonster;
    }

    public void CountKillMonster(Monster monster)
    {
        if (monster.GetIsAlive() == true) return;

        killMonsterCount++;

        if (killMonsterCount < killMonsterCountMax) return;

        Player.Instance.ChangeCurrentHP(recoveryHP);
        killMonsterCount = 0;
    }

    public override void RemoveSkill()
    {
        Player.Instance.hitMonsterFuncList -= CountKillMonster;
    }
}