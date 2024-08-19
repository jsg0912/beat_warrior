using UnityEngine;

public class KillRecoveryHP : PassiveSkill
{
    private int recoveryHP;

    private int killMonsterCount;
    private int killMonsterCountMax;

    public KillRecoveryHP(GameObject unit) : base(unit) { }

    public override void GetSkill()
    {
        recoveryHP = 1;

        killMonsterCount = 0;
        killMonsterCountMax = 3;

        Player.Instance.HitMonsterFuncList += CountkillMonster;
    }

    public void CountkillMonster(MonsterUnit monster)
    {
        if (monster.GetIsAlive() == true) return;

        killMonsterCount++;

        if (killMonsterCount < killMonsterCountMax) return;

        Player.Instance.ChangeCurrentHP(recoveryHP);
        killMonsterCount = 0;
    }

    public override void RemoveSkill()
    {
        Player.Instance.HitMonsterFuncList -= CountkillMonster;
    }
}