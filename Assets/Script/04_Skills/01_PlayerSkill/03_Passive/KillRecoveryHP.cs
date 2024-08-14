public class KillRecoveryHP : PassiveSkill
{
    private int killMonsterCount;
    private int killMonsterCountMax;

    public override void GetSkill()
    {
        killMonsterCount = 0;
        killMonsterCountMax = 10;
    }

    public void CountkillMonster()
    {
        killMonsterCount++;

        if (killMonsterCount < killMonsterCountMax) return;

        Player.Instance.ChangeCurrentHP(1);
        killMonsterCount = 0;
    }
}
