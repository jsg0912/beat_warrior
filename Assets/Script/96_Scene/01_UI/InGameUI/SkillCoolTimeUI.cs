using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTimeUI : MonoBehaviour
{
    [SerializeField] SkillName skillName;
    [SerializeField] Image CoolTimeImg;
    [SerializeField] Image SkillIconUILight;
    [SerializeField] TextMeshProUGUI SkillName; // TODO: 표시 안하는 거로 기획 확정나면 제거 - 신동환, 20250114
    [SerializeField] TextMeshProUGUI CoolTimeText;

    private bool isFirstCoolTime = false; // This Bool is for optimization - SDH, 20250114
    void Start()
    {
        SkillName.text = skillName.ToString();
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
            Util.SetActive(SkillIconUILight.gameObject, true);
            Util.SetActive(CoolTimeText.gameObject, true);
            StartSkillIconLightAnimation();

            isFirstCoolTime = false;
        }
    }

    private void TryTurnOffSkillCoolTimeUI()
    {
        if (isFirstCoolTime == true)
        {
            if (isFirstCoolTime == false)
            {
                Util.SetActive(SkillIconUILight.gameObject, false);
                Util.SetActive(CoolTimeText.gameObject, false);

                isFirstCoolTime = true;
            }
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
}