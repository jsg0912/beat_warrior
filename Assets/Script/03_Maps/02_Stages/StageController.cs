public class StageController
{
    public int monsterCount { get; private set; }
    public bool Cleared => monsterCount == 0;

    public void SetMonsterCount(int monsterCount)
    {
        this.monsterCount = monsterCount;
    }

    public void KillMonster() { monsterCount--; }
}