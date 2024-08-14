using UnityEngine;

public class Skill2 : ActiveSkillPlayer
{
    private float dashRange;

    public override void Initialize()
    {
        skillName = PLAYERSKILLNAME.SKILL2;
        status = PLAYERSTATUS.SKILL2;

        damageMultiplier = PlayerSkillConstant.skill2Atk;
        dashRange = PlayerSkillConstant.skill2DashRange;

        coolTimeMax = PlayerSkillConstant.skill2CoolTimeMax;
        coolTime = 0;

        EffectPrefab = Resources.Load(PlayerSkillConstant.skill2Prefab) as GameObject;
    }

    protected override void UpdateKey()
    {
        keyCode = KeySetting.keys[ACTION.SKILL2];
    }

    protected override void SkillMethod()
    {
        CreateAttackPrefab();

        Vector2 start = Player.Instance.transform.position;
        Vector2 end = start += new Vector2(dashRange, 0.0f) * Player.Instance.GetDirection();

        Player.Instance.Dashing(end, false, false);
    }
}
