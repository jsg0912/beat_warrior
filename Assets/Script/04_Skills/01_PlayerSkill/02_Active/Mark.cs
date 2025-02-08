using System.Collections;
using UnityEngine;

public class Mark : ActiveSkillPlayer
{
    private GameObject MarkerPrefab;
    private Timer markSlowTimer;

    public Mark(GameObject unit) : base(unit)
    {
        trigger = new() { PlayerConstant.markAnimTrigger };
        MarkerPrefab = Resources.Load(PrefabRouter.MarkerPrefab) as GameObject;
        markSlowTimer = new Timer(PlayerSkillConstant.MarkSlowDuration);
    }

    protected override void SetSkillName() { skillName = SkillName.Mark; }

    public override void CheckInputKeyCode()
    {
        UpdateKey();

        if (coolTime <= 0 && Input.GetKeyDown(keyCode))
        {
            markSlowTimer.Initialize();
            PauseController.instance.SetZoomInSlow();
            // TODO: Cursor Change;
        }
        else if (markSlowTimer.remainTime > 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                markSlowTimer.SetRemainTimeZero();
                PauseController.instance.ResetSpeed();
                // TODO: Cursor Change;
            }
            if (Input.GetKey(keyCode))
            {
                if (!markSlowTimer.UnScaledTick())
                {
                    PauseController.instance.ResetSpeed();
                }
            }
            else if (Input.GetKeyUp(keyCode))
            {
                if (Player.Instance.IsActionAble())
                {
                    PauseController.instance.ResetSpeed();
                    TrySkill();
                    // TODO: CursorReset;
                }
            }
        }
    }

    // TODO: UpdateKey 사용 시점 수정(지금 무슨 실행될떄마다 실행되고 있음 쓸데없이)
    protected override void UpdateKey()
    {
        keyCode = KeySetting.keys[PlayerAction.Mark_Dash];
    }

    protected override IEnumerator CountCoolTime()
    {
        yield return base.CountCoolTime();
        PlayerUIManager.Instance.SwapMarkAndDash(true);
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
