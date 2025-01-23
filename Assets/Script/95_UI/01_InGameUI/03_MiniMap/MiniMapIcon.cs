using UnityEngine;
using TMPro;

public class MiniMapIcon : MonoBehaviour
{
    public TMP_Text hpText;
    public int hp;

    private PoolTag poolTag = PoolTag.EnemyMiniMapIcon;

    Vector3 Target;

    private void Update()
    {
        this.transform.position = Target;
        if (hp == 0) MyPooler.ObjectPooler.Instance.ReturnToPool(poolTag, this.gameObject);
    }

    public void GetHp(int unitHp)
    {
        hp = unitHp;
        hpText.text = unitHp.ToString();
    }

    public void GetTarget(Vector3 pos)
    {
        Target = pos;
    }
}
