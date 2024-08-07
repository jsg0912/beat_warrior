using UnityEngine;

public class Mark : PlayerSkill
{
    private GameObject MarkerPrefab;

    public override void Initialize()
    {
        skillName = PLAYERSKILLNAME.MARK;
        status = PLAYERSTATUS.MARK;
        animTrigger = PlayerSkillConstant.markAnimTrigger;

        cooltimeMax = PlayerSkillConstant.dashCoolTimeMax;
        cooltime = 0;

        MarkerPrefab = Resources.Load("Prefab/Marker") as GameObject;
    }

    protected override void UpdateKey()
    {
        key = KeySetting.keys[ACTION.MARK];
    }

    protected override void SkillMethod()
    {
        Transform playerTransform = Player.Instance.transform;

        Vector2 start = playerTransform.position + new Vector3(0, 0.5f, 0);
        Vector2 end = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameObject Marker = GameObject.Instantiate(MarkerPrefab, start, Quaternion.identity);

        Marker.GetComponent<Marker>().SetVelocity(start, end);
    }
}
