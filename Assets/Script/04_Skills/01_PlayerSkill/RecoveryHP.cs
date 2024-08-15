using System.Collections;
using UnityEngine;

public class RecoveryHP : ActiveSkillPlayer
{
    public RecoveryHP(GameObject unit) : base(unit) { }

    public override void GetSkill()
    {
        skillName = SkillName.KillRecoveryHP;

        coolTimeMax = PlayerSkillConstant.recoveryHPTimeMax;
        coolTime = coolTimeMax;
    }

    protected override IEnumerator CountCoolTime()
    {
        coolTime = coolTimeMax;

        while (coolTime > 0)
        {
            coolTime -= Time.deltaTime;
            yield return null;
        }

        Player.Instance.ChangeCurrentHP(1);

        if (Player.Instance.GetIsFullHP() == false)
            unit.GetComponent<MonoBehaviour>().StartCoroutine(CountCoolTime());
    }

    protected override void SkillMethod() { }

    protected override void UpdateKey() { }
}
