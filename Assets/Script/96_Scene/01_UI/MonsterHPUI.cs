using TMPro;
using UnityEngine;

public class MonsterHPUI : MonoBehaviour
{
    [SerializeField] Transform hpTransform;
    [SerializeField] TextMeshPro hpText;

    private int hpMax;
    private int hpNow;

    public void SetMaxHP(int hpMax)
    {
        this.hpMax = hpMax;
        hpNow = hpMax;

        hpText.text = hpNow.ToString();
    }

    public void SetHP(int hp)
    {
        if (hp == 0) Destroy(this.gameObject);

        hpNow = hp;

        hpText.text = hpNow.ToString();
        hpTransform.localScale = new Vector3((float)hpNow / hpMax, 0.1f, 1.0f);
    }
}
