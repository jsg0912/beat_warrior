using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject HP;

    [SerializeField] private Image MarkImg;
    [SerializeField] private Image DashImg;
    [SerializeField] private Image Skill1Img;
    [SerializeField] private Image Skill2Img;

    public TextMeshProUGUI[] txt;



    private void Start()
    {
        for (int i = 0; i < txt.Length; i++)
        {
            txt[i].text = KeySetting.keys[(ACTION)i].ToString();
        }
        
    }

    private void Update()
    {
        UpdateHP();
        UpdateCoolTime();
        

        for (int i = 0; i < txt.Length; i++)
        {
            txt[i].text = KeySetting.keys[(ACTION)i].ToString();
        }
    }

    private void UpdateHP()
    {
        int hp = Player.Instance.GetHP();

        HP.transform.GetChild(0).gameObject.SetActive(hp > 2);
        HP.transform.GetChild(1).gameObject.SetActive(hp > 1);
        HP.transform.GetChild(2).gameObject.SetActive(hp > 0);
    }

    private void UpdateCoolTime()
    {
        MarkImg.GetComponent<Image>().fillAmount = 1 - Player.Instance.GetMarkCoolTime() / Player.markCoolTimeMax;
        DashImg.GetComponent<Image>().fillAmount = 1 - Player.Instance.GetDashCoolTime() / Player.dashCoolTimeMax;
        Skill1Img.GetComponent<Image>().fillAmount = 1 - Player.Instance.GetSkill1CoolTime() / Player.skill1CoolTimeMax;
        Skill2Img.GetComponent<Image>().fillAmount = 1 - Player.Instance.GetSkill2CoolTime() / Player.skill2CoolTimeMax;
    }
}
