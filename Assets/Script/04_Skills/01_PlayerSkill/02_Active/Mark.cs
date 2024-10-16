using UnityEngine;

public class Mark : ActiveSkillPlayer
{
    private GameObject MarkerPrefab;

    public Mark(GameObject unit) : base(unit)
    {
        skillName = SkillName.Mark;
        status = PlayerStatus.Mark;

        coolTimeMax = PlayerSkillConstant.SkillCoolTime[skillName];
        coolTime = 0;

        MarkerPrefab = Resources.Load(PrefabRouter.MarkerPrefab) as GameObject;
    }

    protected override void UpdateKey()
    {
        keyCode = KeySetting.keys[Action.Mark_Dash];
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
