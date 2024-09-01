using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public static SkillUI Instance;

    [SerializeField] Text AttackCountView;

    [SerializeField] private List<Image> SkillCoolTimeImgList;
    private Dictionary<SkillName, Image> SkillCoolTimeImg = new();

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        
        SkillCoolTimeImg.Add(SkillName.Mark, SkillCoolTimeImgList[0]);
        SkillCoolTimeImg.Add(SkillName.Dash, SkillCoolTimeImgList[1]);
        SkillCoolTimeImg.Add(SkillName.Skill1, SkillCoolTimeImgList[2]);
        SkillCoolTimeImg.Add(SkillName.Skill2, SkillCoolTimeImgList[3]);
        SkillCoolTimeImg.Add(SkillName.Attack, SkillCoolTimeImgList[4]);
    }

    private void Update()
    {
        UpdateCoolTime();
    }

    private void UpdateCoolTime()
    {
        foreach (var skill in SkillCoolTimeImg)
            skill.Value.fillAmount
                = 1 - Player.Instance.GetSkillCoolTime(skill.Key) / PlayerSkillConstant.SkillCoolTime[skill.Key];


        AttackCountView.text = Player.Instance.GetCurrentStat(StatKind.AttackCount).ToString();
    }
}
