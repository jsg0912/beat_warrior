using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Skill2 : PlayerSkill
{
    private float dashRange;

    public override void Initialize()
    {
        skillName = PLAYERSKILLNAME.SKILL2;
        status = PLAYERSTATUS.SKILL2;
        animTrigger = PlayerSkillConstant.skill2AnimTrigger;

        atk = PlayerSkillConstant.skill2Atk;
        dashRange = PlayerSkillConstant.skill2DashRange;

        cooltimeMax = PlayerSkillConstant.skill2CoolTimeMax;
        cooltime = 0;

        AttackPrefab = Resources.Load(PlayerSkillConstant.skill2Prefab) as GameObject;
    }

    protected override void UpdateKey()
    {
        key = KeySetting.keys[ACTION.SKILL2];
    }

    protected override void SkillMethod()
    {
        CreateAttackPrefab();

        Player.Instance.GetComponent<MonoBehaviour>().StartCoroutine(Dashing());
    }

    private IEnumerator Dashing()
    {
        Player.Instance.SetInvincibility(true);
        Player.Instance.SetGravity(false);

        Transform playerTransform = Player.Instance.transform;

        Vector2 start = playerTransform.position;
        Vector2 end = start += new Vector2(dashRange, 0.0f) * Player.Instance.GetDirection();

        while (Vector2.Distance(end, playerTransform.position) >= 0.05f)
        {
            playerTransform.position = Vector2.Lerp(playerTransform.position, end, 0.03f);
            yield return null;
        }

        playerTransform.position = end;

        Player.Instance.SetInvincibility(false);
        Player.Instance.SetGravity(true);

        Player.Instance.SetPlayerStatus(PLAYERSTATUS.IDLE);
    }
}
