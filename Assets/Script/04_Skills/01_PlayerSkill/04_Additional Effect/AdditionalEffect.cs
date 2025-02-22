using UnityEngine;

public delegate void HitMonsterFunc(Monster monster);
public delegate void UseSkillFunc(Skill skill);
public delegate bool ReviveSkillFunc();

public abstract class AdditionalEffect
{
    public AdditionalEffectName additionalEffectName
    {
        get;
        protected set;
    }
    public bool canDuplicate
    {
        get;
        private set;
    }
    public abstract void work(GameObject obj);

    public AdditionalEffect()
    {
        SetAdditionalEffectName();
        canDuplicate = PlayerSkillConstant.AdditionalEffectCanDuplicate[additionalEffectName];
    }

    // You muse set the additionalEffectName
    public abstract void SetAdditionalEffectName();
}
