using UnityEngine;

public class Mark : ActiveSkillPlayer
{
    private GameObject MarkerPrefab;

    public Mark(GameObject unit) : base(unit, SkillTier.Normal)
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

        Vector2 start = playerTransform.position + new Vector3(0, 0.5f, 0);
        Vector2 end = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameObject Marker = GameObject.Instantiate(MarkerPrefab, start, Quaternion.identity);

        Vector2 direction = end - start;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Marker.transform.rotation = Quaternion.Euler(0, 0, angle);

        Marker.GetComponent<Marker>().SetVelocity(start, end);
    }
}
