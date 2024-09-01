using UnityEngine;

public class KillRecoveryHP : PassiveSkill
{
    private int recoveryHP;

    private int killMonsterCount;
    private int killMonsterCountMax;

    public KillRecoveryHP(GameObject unit) : base(unit) { }

    public override void GetSkill()
    {
        skillName = SkillName.KillRecoveryHP;

        recoveryHP = 1;

        killMonsterCount = 0;
        killMonsterCountMax = 10;

        Player.Instance.HitMonsterFuncList += CountKillMonster;
    }

    public void CountKillMonster(MonsterUnit monster)
    {
        if (monster.GetIsAlive() == true) return;

        killMonsterCount++;

        if (killMonsterCount < killMonsterCountMax) return;

        Player.Instance.ChangeCurrentHP(recoveryHP);
        killMonsterCount = 0;
    }

    public override void RemoveSkill()
    {
        Player.Instance.HitMonsterFuncList -= CountKillMonster;
    }
}