using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHPUI : MonoBehaviour
{
    [SerializeField] private GameObject hpPrefab;
    private int hpNow = 0;

    public void SetHP(int hp)
    {
        if (hpNow > hp)
        {
            for (int i = 0; i < hpNow - hp; i++)
            {
                GameObject HP = Instantiate(hpPrefab);
                HP.transform.SetParent(this.transform, false);
            }
        }


    }
}
