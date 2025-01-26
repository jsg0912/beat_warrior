using UnityEngine;

public class Skill2 : ActiveSkillPlayer
{
    private float dashRange;

    public Skill2(GameObject unit) : base(unit)
    {
        skillName = SkillName.Skill2;
        status = PlayerStatus.Skill2;

        damageMultiplier = PlayerSkillConstant.skill2Atk;
        dashRange = PlayerSkillConstant.skill2DashRange;

        coolTimeMax = PlayerSkillConstant.SkillCoolTime[skillName];
        coolTime = 0;

        EffectPrefab = Resources.Load(PrefabRouter.Skill2Prefab) as GameObject;
    }

    protected override void UpdateKey()
    {
        keyCode = KeySetting.keys[PlayerAction.Skill2];
    }

    protected override void SkillMethod()
    {
        CreateEffectPrefab();

        Vector2 start = Player.Instance.transform.position;
        Vector2 end = start += new Vector2(dashRange, 0.0f) * Player.Instance.GetMovingDirectionFloat();

        Player.Instance.Dashing(end, false, false, false);
    }
}
