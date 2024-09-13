using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public static SkillUI Instance;

    [SerializeField] Text AttackCountView;

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
