using TMPro;
using UnityEngine;

public class MonsterHPUI : MonoBehaviour
{
    [SerializeField] Transform hpTransform;
    [SerializeField] protected TextMeshPro hpText;
    private float scaleX;

    protected virtual void SetText(int hp, int hpMax)
    {
        hpText.text = hp.ToString();
    }

    public void SetMaxHP(int hpMax)
    {
        SetText(hpMax, hpMax);
        scaleX = hpTransform.localScale.x;
    }

    public void SetHP(int hp, int hpMax)
    {
        if (this == null) return;
        if (hp == 0) Destroy(this.gameObject);

        SetText(hp, hpMax);
        hpTransform.localScale = new Vector3(scaleX * (float)hp / hpMax, hpTransform.localScale.y, 1.0f);
    }
}
