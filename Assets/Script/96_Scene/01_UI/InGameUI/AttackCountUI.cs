using UnityEngine;
using UnityEngine.UI;

public class AttackCountUI : MonoBehaviour
{
    [SerializeField] Text AttackCountView;

    private void Update()
    {
        ViewAttackCount();
    }
    private void ViewAttackCount()
    {
        AttackCountView.text = Player.Instance.GetCurrentStat(StatKind.AttackCount).ToString();
    }
}
