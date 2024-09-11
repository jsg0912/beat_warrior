using UnityEngine;

public class PrototypeManager : MonoBehaviour
{
    public PrototypeManager instance;

    // public Monster[] archerMonsters = new Monster[2];
    // public Monster[] soldierMonsters = new Monster[6];

    public void Awake()
    {
        instance = this;

        // foreach (Monster archerMonster in archerMonsters)
        // {
        //     archerMonster
        // }
    }
}
