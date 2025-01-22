using UnityEngine;

public class StageController
{
    public int monsterCount { get; private set; }
    public bool Cleared => monsterCount == 0;

    public void SetMonsterCount(int monsterCount)
    {
        this.monsterCount = monsterCount;
        Debug.Log($"Found {this.monsterCount} monsters in the stage");

        if (monsterCount > 0)
        {
            Debug.Log($"{monsterCount} monsters remaining");
        }
        if (monsterCount == 0)
        {
            Debug.Log("Stage Cleared!");
        }
    }

    public void KillMonster() { monsterCount--; }
}