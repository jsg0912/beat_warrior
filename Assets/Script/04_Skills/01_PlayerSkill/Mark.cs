using UnityEngine;

public class Mark : Skill
{
    private GameObject MarkerPrefab;

    void Start()
    {
        status = PLAYERSTATUS.MARK;
        animTrigger = PlayerSkillConstant.markAnimTrigger;

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
        Vector3 start = transform.position + new Vector3(0, 0.5f, 0);
        Vector3 end = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameObject Marker = Instantiate(MarkerPrefab, start, Quaternion.identity);

        Marker.GetComponent<Marker>().SetVelocity(start, end);
    }
}
