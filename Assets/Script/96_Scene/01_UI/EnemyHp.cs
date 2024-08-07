using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    private SpriteRenderer[] render;
    private Color[] hpColor;
    public int Enemyhp;
    public int colorNum;
    public int hpNum;

    // Start is called before the first frame update
    void Start()
    {
        render = new SpriteRenderer[5];

        for(int i=0;i<5;i++)
        {
            render[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
        }

        hpColor = new Color[4];

        hpColor[0] = Color.clear;
        hpColor[1] = Color.red;
        hpColor[2] = Color.green;
        hpColor[3] = Color.blue;


    }

    // Update is called once per frame
    void Update()
    {
        UpdateHp1();
    }

    public void UpdateHp()
    {
        colorNum = Enemyhp / 5;
        hpNum = Enemyhp % 5;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < hpNum; j++)
            {
                render[j].color = hpColor[colorNum + 1];
            }
            for (int k = hpNum; k < 5; k++)
            {
                render[k].color = hpColor[colorNum + 1];
            }


        }
    }

    public void UpdateHp1()
    {
        switch(hpNum)
        {
            case 1:
                for (int i = 0; i < 1; i++)
                {
                    render[i].color = hpColor[colorNum + 1];
                }
                for (int i = 1; i < 5; i++)
                {
                    render[i].color = hpColor[colorNum];
                }
                break;
            case 2:
                for (int i = 0; i < 2; i++)
                {
                    render[i].color = hpColor[colorNum + 1];
                }
                for (int i = 2; i < 5; i++)
                {
                    render[i].color = hpColor[colorNum];
                }
                break;
            case 3:
                for (int i = 0; i < 3; i++)
                {
                    render[i].color = hpColor[colorNum + 1];
                }
                for (int i = 3; i < 5; i++)
                {
                    render[i].color = hpColor[colorNum];
                }
                break;
            case 4:
                for (int i = 0; i < 4; i++)
                {
                    render[i].color = hpColor[colorNum + 1];
                }
                for (int i = 4; i < 5; i++)
                {
                    render[i].color = hpColor[colorNum];
                }
                break;
            case 0:
                for (int i = 0; i < 5; i++)
                {
                    render[i].color = hpColor[colorNum ];
                }
                break;
        }


        
    }


    public void SetHp(int EHp)
    {
        Enemyhp = EHp;
        colorNum = Enemyhp / 5;
        hpNum = Enemyhp % 5;
    }
}
