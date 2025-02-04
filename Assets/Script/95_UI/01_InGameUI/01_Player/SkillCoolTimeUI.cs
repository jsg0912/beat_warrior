using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTimeUI : MonoBehaviour
{
    [SerializeField] SkillName skillName;
    [SerializeField] Image CoolTimeImg;
    [SerializeField] Image SkillIconUILight;
    [SerializeField] TextMeshProUGUI SkillHotKey;
    [SerializeField] TextMeshProUGUI CoolTimeText;

    private bool isFirstCoolTime = false; // This Bool is for optimization - SDH, 20250114

    void Start()
    {
        UpdateSkillHotKey();
    }

    // [Code Review - KMJ] Using Coroutine, not Update - SDH, 20250114
    void Update()
    {
        float coolTime = Player.Instance.GetSkillCoolTime(skillName);
        if (coolTime > 0)
        {
            TryTurnOnSkillCoolTimeUI();
            CoolTimeImg.fillAmount = 1 - coolTime / PlayerSkillConstant.SkillCoolTime[skillName];
            CoolTimeText.text = Mathf.Ceil(coolTime).ToString();
        }
        else TryTurnOffSkillCoolTimeUI();
    }

    private void TryTurnOnSkillCoolTimeUI()
    {
        if (isFirstCoolTime == true)
        {
            isFirstCoolTime = false;
            Util.SetActive(SkillIconUILight?.gameObject, true);
            Util.SetActive(CoolTimeText.gameObject, true);
            StartSkillIconLightAnimation();
        }
    }

    private void TryTurnOffSkillCoolTimeUI()
    {
        if (!isFirstCoolTime)
        {
            isFirstCoolTime = true;
            Util.SetActive(SkillIconUILight?.gameObject, false);
            Util.SetActive(CoolTimeText.gameObject, false);
            CoolTimeImg.fillAmount = 1;
        }
    }

    private void StartSkillIconLightAnimation()
    {
        if (SkillIconUILight != null)
        {
            Animator animator = SkillIconUILight.GetComponent<Animator>();
            animator.SetTrigger("Start");
            animator.speed = 1 / PlayerSkillConstant.SkillCoolTime[skillName];
        }
    }

    public void UpdateSkillHotKey()
    {
        // TODO: HotKey 설정된 것에 따라 바뀌게 해야함
        // SkillHotKey.text = skillName.ToString(); 
    }
}