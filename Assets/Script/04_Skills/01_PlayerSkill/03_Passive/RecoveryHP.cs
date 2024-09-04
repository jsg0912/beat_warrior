using System.Collections;
using UnityEngine;

public class RecoveryHP : ActiveSkillPlayer
{
    public RecoveryHP(GameObject unit) : base(unit)
    {
        skillName = SkillName.KillRecoveryHP;
        coolTime = PlayerSkillConstant.recoveryHPTimeMax;
    }

    protected override IEnumerator CountCoolTime()
    {
        coolTime = PlayerSkillConstant.recoveryHPTimeMax;

        while (coolTime > 0)
        {
            coolTime -= Time.deltaTime;
            yield return null;
        }

        Player.Instance.ChangeCurrentHP(1);

        if (Player.Instance.playerUnit.GetIsFullStat(StatKind.HP) == false)
            unit.GetComponent<MonoBehaviour>().StartCoroutine(CountCoolTime());
    }

    protected override void SkillMethod() { }

    protected override void UpdateKey() { }
}
