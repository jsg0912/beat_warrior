using UnityEngine;
using UnityEngine.Pool;
using TMPro;

public class MiniMapIcon : MonoBehaviour
{
    public IObjectPool<GameObject> Pool { get; set; }

    public TMP_Text hpText;
    public int hp;

    Vector3 Target;

    private void Update()
    {
        this.transform.position = Target;
        if(hp == 0) Pool.Release(this.gameObject);
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
