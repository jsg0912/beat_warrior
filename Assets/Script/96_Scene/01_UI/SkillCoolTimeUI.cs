using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SkillCoolTimeUI : MonoBehaviour
{
    [SerializeField] SkillName skillName;
    [SerializeField] Image CoolTimeImg;
    [SerializeField] TextMeshProUGUI SkillName;
    [SerializeField] TextMeshProUGUI CoolTimeText;

    void Start()
    {
        SkillName.text = skillName.ToString();
    }

    void Update()
    {
        float coolTime = Player.Instance.GetSkillCoolTime(skillName);
        CoolTimeImg.fillAmount = 1 - coolTime / PlayerSkillConstant.SkillCoolTime[skillName];
        CoolTimeText.gameObject.SetActive(coolTime != 0);
        CoolTimeText.text = Mathf.Ceil(coolTime).ToString();
    }
}
