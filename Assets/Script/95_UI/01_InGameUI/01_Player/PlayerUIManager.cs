using UnityEngine;

public class PlayerUIManager : SingletonObject<PlayerUIManager>
{
    [SerializeField] private PlayerHpUIController playerHpUI;
    [SerializeField] private SkillCoolTimeUI SkillCoolTimeUIMark;
    [SerializeField] private SkillCoolTimeUI SkillCoolTimeUIDash;
    [SerializeField] private SkillCoolTimeUI SkillCoolTimeUISpecialBlade;
    [SerializeField] private SkillCoolTimeUI SkillCoolTimeUISweepingBlade;
    [SerializeField] private SkillCoolTimeUI SkillCoolTimeUIHollyBlade;
    [SerializeField] private AttackCountUI attackCountUI;
    [SerializeField] private PlayerFaceController playerFaceController;

    void Start()
    {
        Initialize();
    }

    void OnEnable()
    {
        Initialize();
    }

    public void Initialize()
    {
        playerHpUI.Initialize();
        attackCountUI.UpdateUI();
        SkillCoolTimeUISpecialBlade.ResetSkillCoolDownUI();
        SkillCoolTimeUISweepingBlade.ResetSkillCoolDownUI();
        SkillCoolTimeUIHollyBlade.ResetSkillCoolDownUI();
        SwapMarkAndDash(true);
    }

    public void SwapMarkAndDash(bool isMarkOn)
    {
        if (isMarkOn)
        {
            ResetCoolTImeUI(SkillName.Dash);
            Util.SetActive(SkillCoolTimeUIDash.gameObject, false);
            Util.SetActive(SkillCoolTimeUIMark.gameObject, true);
            SetCoolTimeUI(SkillName.Mark);
        }
        else
        {
            ResetCoolTImeUI(SkillName.Mark);
            Util.SetActive(SkillCoolTimeUIMark.gameObject, false);
            Util.SetActive(SkillCoolTimeUIDash.gameObject, true);
            SetCoolTimeUI(SkillName.Dash);
        }
    }

    public void SetPlayerFace(PlayerStatus playerStatus, int hp)
    {
        playerFaceController.SetPlayerFace(playerStatus, hp);
    }

    public void SetCoolTimeUI(SkillName skillName)
    {
        switch (skillName)
        {
            case SkillName.Attack:
                SkillCoolTimeUIHollyBlade.TryCoolDownAnimation();
                break;
            case SkillName.Skill1:
                SkillCoolTimeUISpecialBlade.TryCoolDownAnimation();
                break;
            case SkillName.Skill2:
                SkillCoolTimeUISweepingBlade.TryCoolDownAnimation();
                break;
            case SkillName.Mark:
                SkillCoolTimeUIMark.TryCoolDownAnimation();
                break;
            case SkillName.Dash:
                SkillCoolTimeUIDash.TryCoolDownAnimation(false, false);
                break;
        }
    }

    public void ResetCoolTImeUI(SkillName skillName)
    {
        switch (skillName)
        {
            case SkillName.Attack:
                SkillCoolTimeUIHollyBlade.ResetSkillCoolDownUI();
                break;
            case SkillName.Skill1:
                SkillCoolTimeUISpecialBlade.ResetSkillCoolDownUI();
                break;
            case SkillName.Skill2:
                SkillCoolTimeUISweepingBlade.ResetSkillCoolDownUI();
                break;
            case SkillName.Mark:
                SkillCoolTimeUIMark.ResetSkillCoolDownUI();
                break;
            case SkillName.Dash:
                SkillCoolTimeUIDash.ResetSkillCoolDownUI();
                break;
        }
    }

    public void UpdateHotKeyText(PlayerAction playerAction)
    {
        switch (playerAction)
        {
            case PlayerAction.Attack:
                SkillCoolTimeUIHollyBlade.UpdateSkillHotKey();
                break;
            case PlayerAction.Skill1:
                SkillCoolTimeUISpecialBlade.UpdateSkillHotKey();
                break;
            case PlayerAction.Skill2:
                SkillCoolTimeUISweepingBlade.UpdateSkillHotKey();
                break;
            case PlayerAction.Mark_Dash:
                SkillCoolTimeUIMark.UpdateSkillHotKey();
                SkillCoolTimeUIDash.UpdateSkillHotKey();
                break;
        }
    }
}