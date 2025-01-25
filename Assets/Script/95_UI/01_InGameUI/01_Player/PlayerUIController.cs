using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    public static PlayerUIController Instance;
    [SerializeField] private PlayerHpUI playerHpUI;
    [SerializeField] private SkillCoolTimeUI SkillCoolTimeUIMark;
    [SerializeField] private SkillCoolTimeUI SkillCoolTimeUIDash;
    [SerializeField] private SkillCoolTimeUI SkillCoolTimeUISpecialBlade;
    [SerializeField] private SkillCoolTimeUI SkillCoolTimeUISweepingBlade;
    [SerializeField] private SkillCoolTimeUI SkillCoolTimeUIHollyBlade;
    [SerializeField] private AttackCountUI attackCountUI;

    void Awake()
    {
        Instance = this;
    }

    public void Initialize()
    {
        playerHpUI.Initialize();
        // TODO: 현재는 skillCoolTimeUI가 Update 문을 통해 Update되고 있는데, coroutine 사용으로 바뀔 경우 여기서 초기화해주는 Code 필요 - SDH, 20250114
        // foreach (SkillCoolTimeUI skillCoolTimeUI in skillCoolTimeUIs)
        // {
        //     skillCoolTimeUI.initialize();
        // }
        attackCountUI.UpdateUI();
    }
}