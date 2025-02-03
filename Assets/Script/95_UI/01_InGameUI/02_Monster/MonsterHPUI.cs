using TMPro;
using UnityEngine;

public class MonsterHPUI : MonoBehaviour
{
    [SerializeField] Transform hpTransform;
    [SerializeField] TextMeshPro hpText;
    private float scaleX;

    public void SetMaxHP(int hpMax)
    {
        hpText.text = hpMax.ToString();
        scaleX = hpTransform.localScale.x;
    }

    public void SetHP(int hp, int hpMax)
    {
        if (this == null) return;
        if (hp == 0) Destroy(this.gameObject);

        hpText.text = hp.ToString();
        hpTransform.localScale = new Vector3(scaleX * (float)hp / hpMax, hpTransform.localScale.y, 1.0f);
    }
}
