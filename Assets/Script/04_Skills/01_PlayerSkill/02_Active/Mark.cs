using System.Collections;
using UnityEngine;

public class Mark : ActiveSkillPlayer
{
    private GameObject MarkerPrefab;
    private Timer markSlowTimer;

    public Mark(GameObject unit) : base(unit)
    {
        MarkerPrefab = Resources.Load(PrefabRouter.MarkerPrefab) as GameObject;
        markSlowTimer = new Timer(TimeScaleConstant.MarkSlowDuration);
    }

    protected override void SetSkillName() { skillName = SkillName.Mark; }

    public override void CheckInputKeyCode()
    {
        UpdateKey();

        if (coolTime <= 0 && Input.GetKeyDown(keyCode)) ZoomIn();
    }

    public override void CheckFixedInputKeyCode()
    {
        if (markSlowTimer.remainTime > 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)) ZoomOut();
            if (Input.GetKey(keyCode))
            {
                if (!markSlowTimer.UnScaledTick()) ZoomOut();
            }
            if (Input.GetKeyUp(keyCode))
            {
                if (Player.Instance.IsActionAble())
                {
                    TrySkill();
                }
                ZoomOut();
            }
        }
    }

    private void ZoomIn()
    {
        CursorController.Instance.SetZoomInCursor();
        markSlowTimer.Initialize();
        PauseController.Instance.SetZoomInSlow();
    }

    private void ZoomOut()
    {
        GameManager.Instance.SetDefaultCursor();
        PauseController.Instance.ResetSpeed();
        markSlowTimer.SetRemainTimeZero();
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
        Util.RotateObjectForwardingDirection(Marker, direction, true);
        Marker.GetComponent<Marker>().SetVelocity(start, end);

        SoundManager.Instance.SFXPlay("PlayerMark", SoundList.Instance.playerMark);
    }
}
