using System.Collections;
using UnityEngine;

public class GroggyStrategyZeroDef : GroggyStrategy
{
    public GroggyStrategyZeroDef(float groggyTime)
    {
        this.groggyTime = groggyTime;
    }

    public override bool PlayStrategy()
    {
        monster.GetComponent<MonoBehaviour>().StartCoroutine(PlayGroggy());
        return true;
    }

    protected IEnumerator PlayGroggy()
    {
        monster.SetBuffMultiply(StatKind.Def, -1);
        yield return new WaitForSeconds(groggyTime);
        monster.ResetBuffMultiply(StatKind.Def);
        monster.SetStatus(MonsterStatus.Idle);
    }
}