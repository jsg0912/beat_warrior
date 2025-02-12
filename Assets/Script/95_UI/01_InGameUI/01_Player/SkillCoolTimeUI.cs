using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTimeUI : MonoBehaviour
{
    [SerializeField] SkillName skillName;
    [SerializeField] Image CoolTimeImg;
    [SerializeField] Image SkillIconUILight; // It can be null (ex. Passive Skill or Dash) 
    [SerializeField] TextMeshProUGUI SkillHotKey;
    [SerializeField] TextMeshProUGUI CoolTimeText;
    Timer timer = new Timer();
    Coroutine coroutine = null;

    void Start()
    {
        UpdateSkillHotKey();
        // ResetSkillCoolDownUI();
    }

    public void TryCoolDownAnimation()
    {
        float initCoolTime = Player.Instance.GetSkillCoolTime(skillName);
        if (initCoolTime > 0)
        {
            timer.Initialize(initCoolTime);
            coroutine = StartCoroutine(CoolDownAnimation(initCoolTime));
        }
    }

    private IEnumerator CoolDownAnimation(float initCoolTime)
    {
        TryTurnOnSkillCoolDownUI();
        while (timer.Tick())
        {
            float coolTime = Player.Instance.GetSkillCoolTime(skillName);
            CoolTimeImg.fillAmount = 1 - coolTime / initCoolTime;
            CoolTimeText.text = Mathf.Ceil(coolTime).ToString();
            yield return null;
        }
        ResetSkillCoolDownUI();
    }

    public void TryTurnOnSkillCoolDownUI()
    {
        Util.SetActive(CoolTimeText.gameObject, true);
    }

    public void ResetSkillCoolDownUI()
    {
        Util.SetActive(SkillIconUILight?.gameObject, false);
        Util.SetActive(CoolTimeText.gameObject, false);
        CoolTimeImg.fillAmount = 1;
    }

    public void StartSkillIconLightAnimation()
    {
        if (SkillIconUILight != null)
        {
            Util.SetActive(SkillIconUILight?.gameObject, true);
            Animator animator = SkillIconUILight.GetComponent<Animator>();
            animator.SetTrigger("Start");
            animator.speed = 1 / PlayerSkillConstant.SkillCoolTime[skillName];
        }
    }

    public void UpdateSkillHotKey()
    {
        switch (skillName)
        {
            case SkillName.Attack:
                SkillHotKey.text = KeySetting.keys[PlayerAction.Attack].ToString();
                break;
            case SkillName.Mark:
                SkillHotKey.text = KeySetting.keys[PlayerAction.Mark_Dash].ToString();
                break;
            case SkillName.Dash:
                SkillHotKey.text = KeySetting.keys[PlayerAction.Mark_Dash].ToString();
                break;
            case SkillName.Skill1:
                SkillHotKey.text = KeySetting.keys[PlayerAction.Skill1].ToString();
                break;
            case SkillName.Skill2:
                SkillHotKey.text = KeySetting.keys[PlayerAction.Skill2].ToString();
                break;
        }
    }

    public void StopCoolDownAnimation()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }
}