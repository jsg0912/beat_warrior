using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    public static PlayerUIManager Instance { get; private set; }
    void Awake() { Instance = this; }
    [SerializeField] private PlayerHpUIController playerHpUI;
    [SerializeField] private SkillCoolTimeUI SkillCoolTimeUIMark;
    [SerializeField] private SkillCoolTimeUI SkillCoolTimeUIDash;
    [SerializeField] private SkillCoolTimeUI SkillCoolTimeUISpecialBlade;
    [SerializeField] private SkillCoolTimeUI SkillCoolTimeUISweepingBlade;
    [SerializeField] private SkillCoolTimeUI SkillCoolTimeUIHollyBlade;
    [SerializeField] private AttackCountUI attackCountUI;
    [SerializeField] private PlayerFaceController playerFaceController;

    public void Initialize()
    {
        playerHpUI.Initialize();
        // TODO: 현재는 skillCoolTimeUI가 Update 문을 통해 Update되고 있는데, coroutine 사용으로 바뀔 경우 여기서 초기화해주는 Code 필요 - SDH, 20250114
        // foreach (SkillCoolTimeUI skillCoolTimeUI in skillCoolTimeUIs)
        // {
        //     skillCoolTimeUI.initialize();
        // }
        attackCountUI.UpdateUI();
        SwapMarkAndDash(true);
    }

    public void SwapMarkAndDash(bool isMarkOn)
    {
        Util.SetActive(SkillCoolTimeUIMark.gameObject, isMarkOn);
        Util.SetActive(SkillCoolTimeUIDash.gameObject, !isMarkOn);
    }

    public void SetPlayerFace(PlayerStatus playerStatus, int hp)
    {
        if (playerStatus == PlayerStatus.Happy) playerFaceController.SetHappyFace(); // Stage Clear is the most valuable face
        else
        {
            if (hp <= PlayerConstant.PlayerHurtFaceTriggerHp) playerFaceController.SetHurtFace();
            else playerFaceController.SetIdleFace();
        }
    }
}