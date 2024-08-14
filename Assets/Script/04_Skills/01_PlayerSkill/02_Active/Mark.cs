using UnityEngine;

public class Mark : ActiveSkillPlayer
{
    private GameObject MarkerPrefab;

    public override void Initialize()
    {
        skillName = PLAYERSKILLNAME.MARK;
        status = PLAYERSTATUS.MARK;

        coolTimeMax = PlayerSkillConstant.dashCoolTimeMax;
        coolTime = 0;

        MarkerPrefab = Resources.Load("Prefab/Marker") as GameObject;
    }

    protected override void UpdateKey()
    {
        keyCode = KeySetting.keys[ACTION.MARK];
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
