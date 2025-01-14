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

    void Update()
    {
        float coolTime = Player.Instance.GetSkillCoolTime(skillName);
        if (coolTime != 0)
        {
            if (isFirstCoolTime == true)
            {
                Util.SetActive(SkillIconUILight.gameObject, true);
                Util.SetActive(CoolTimeText.gameObject, true);
                isFirstCoolTime = false;
            }
            CoolTimeImg.fillAmount = 1 - coolTime / PlayerSkillConstant.SkillCoolTime[skillName];
            CoolTimeText.gameObject.SetActive(coolTime != 0);
            CoolTimeText.text = Mathf.Ceil(coolTime).ToString();
        }
        else
        {
            if (isFirstCoolTime == false)
            {
                Util.SetActive(SkillIconUILight.gameObject, false);
                Util.SetActive(CoolTimeText.gameObject, false);
                isFirstCoolTime = true;
            }
        }
    }
}
