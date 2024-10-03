using UnityEngine;

public class StageController : MonoBehaviour
{
    private int monsters;
    public bool Cleared { get; private set; }
    public Transform Spawner;

    private void Start()
    {
    }

    public void InitializeStage(int monsterCount)
    {
        monsters = monsterCount;
        Cleared = false;
    }

    public void KillMonster()
    {
        if (monsters > 0)
        {
            monsters--;
            Debug.Log($"{monsters} monsters remaining");
        }
        if (monsters == 0)
        {
            Cleared = true;
            Debug.Log("Stage cleared!");
            ChapterManager.Instance.CompleteStage();
        }
    }

    public void MovePlayerToSpawner()
    {
        Player.Instance.transform.position = Spawner.position;
    }
}
