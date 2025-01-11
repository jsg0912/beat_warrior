using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SkillCoolTimeUI : MonoBehaviour
{
    [SerializeField] SkillName skillName;
    [SerializeField] Image CoolTimeImg;
    [SerializeField] Image SkillIconUILight;
    [SerializeField] TextMeshProUGUI SkillName;
    [SerializeField] TextMeshProUGUI CoolTimeText;

    void Start()
    {
        SkillName.text = skillName.ToString();
    }

    void Update()
    {
        float coolTime = Player.Instance.GetSkillCoolTime(skillName);
        if (coolTime != 0)
        {
            if (SkillIconUILight != null && SkillIconUILight.IsActive())
            {
                SkillIconUILight.gameObject.SetActive(false);
            }
            CoolTimeImg.fillAmount = 1 - coolTime / PlayerSkillConstant.SkillCoolTime[skillName];
            CoolTimeText.gameObject.SetActive(coolTime != 0);
            CoolTimeText.text = Mathf.Ceil(coolTime).ToString();
        }
        if (SkillIconUILight != null && SkillIconUILight.IsActive() == false)
        {
            SkillIconUILight.gameObject.SetActive(true);
        }
    }
}
