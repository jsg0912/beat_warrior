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
        keyCode = KeySetting.keys[PlayerAction.Mark_Dash];
    }

    protected override void SkillMethod()
    {
        Transform playerTransform = Player.Instance.transform;

        Vector3 start = playerTransform.position + new Vector3(0, 0.5f, 0);
        Vector3 end = Util.GetMousePointWithPerspectiveCamera();
        end.z = 0;
        GameObject Marker = GameObject.Instantiate(MarkerPrefab, start, Quaternion.identity);

        Vector3 direction = end - start;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Marker.transform.rotation = Quaternion.Euler(0, 0, angle);

        Marker.GetComponent<Marker>().SetVelocity(start, end);
    }
}
