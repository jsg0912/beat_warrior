using System.Collections.Generic;

[System.Serializable]
public class MonsterJSON
{
    public string monsterName;
    public string description;
    public string[] statKinds;
    public int[] statValues;
    public string patternName;

    public MonsterJSON(MonsterName monsterName, string description, Dictionary<StatKind, int> unitStats, PatternName patternName)
    {
        this.monsterName = monsterName.ToString();
        this.description = description;
        statKinds = new string[unitStats.Count];
        statValues = new int[unitStats.Count];
        int index = 0;
        foreach (KeyValuePair<StatKind, int> entry in unitStats)
        {
            statKinds[index] = entry.Key.ToString();
            statValues[index] = entry.Value;
            index++;
        }
        this.patternName = patternName.ToString();
    }
}