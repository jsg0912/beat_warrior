using UnityEngine;

public abstract class ActiveSkill : Skill
{
    protected ActiveSkill(GameObject unit) : base(unit) { }

    protected virtual void TrySkill()
    {
        if (coolTime > 0) return;

        UseSkill();

        unit.GetComponent<MonoBehaviour>().StartCoroutine(CountCoolTime());
    }

    protected abstract void UseSkill();

    protected abstract void SkillMethod();
}