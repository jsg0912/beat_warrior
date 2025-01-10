using TMPro;
using UnityEngine;

public class MonsterHPUI : MonoBehaviour
{
    [SerializeField] Transform hpTransform;
    [SerializeField] TextMeshPro hpText;

    public void SetMaxHP(int hpMax)
    {
        hpText.text = hpMax.ToString();
    }

    public void SetHP(int hp, int hpMax)
    {
        if (hp == 0) Destroy(this.gameObject);

        hpText.text = hp.ToString();
        hpTransform.localScale = new Vector3((float)hp / hpMax, 0.1f, 1.0f);
    }
}
