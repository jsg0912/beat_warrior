using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHPUI : MonoBehaviour
{
    [SerializeField] private GameObject hpPrefab;
    private int hpNow = 0;

    public void SetHP(int hp)
    {
        if (hp > hpNow)
        {
            for (int i = 0; i < hp - hpNow; i++)
            {
                GameObject HP = Instantiate(hpPrefab);
                HP.transform.SetParent(this.transform, false);
            }

            return;
        }

        for (int i = 0; i < hpNow - hp; i++)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}