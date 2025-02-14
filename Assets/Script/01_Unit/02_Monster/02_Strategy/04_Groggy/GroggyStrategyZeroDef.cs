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

    // TODO 아래 함수 일부분은 Common화 필요
    protected IEnumerator PlayGroggy()
    {
        monster.SetBuffMultiply(StatKind.Def, -1);
        yield return new WaitForSeconds(groggyTime);
        monster.ResetBuffMultiply(StatKind.Def);
        monster.PlayAnimation(MonsterStatus.Groggy, false);
        monster.SetStatus(MonsterStatus.Idle);
    }
}