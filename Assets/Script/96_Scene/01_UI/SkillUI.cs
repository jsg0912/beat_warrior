using System.Collections.Generic;
using UnityEngine;
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
    }

    private void Update()
    {
        UpdateCoolTime();
    }

    private void UpdateCoolTime()
    {
        AttackCountView.text = Player.Instance.GetCurrentStat(StatKind.AttackCount).ToString();
    }
}
