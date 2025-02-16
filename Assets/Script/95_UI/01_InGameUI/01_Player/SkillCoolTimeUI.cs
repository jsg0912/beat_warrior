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
    Timer lightTimer = new Timer();
    Timer gaugeTimer = new Timer();
    Timer textTimer = new Timer();
    float maxCoolTime;

    void Start()
    {
        maxCoolTime = PlayerSkillConstant.SkillCoolTime[skillName];
        UpdateSkillHotKey();
    }

    public void TryCoolDownAnimation(bool isOnGauge = true, bool isOnText = true, bool isOnLight = true)
    {
        float initCoolTime = Player.Instance.GetSkillCoolTime(skillName);
        if (initCoolTime > 0)
        {
            if (isOnText && CoolTimeText != null)
            {
                StartCoroutine(CoolDownAnimationText(initCoolTime));
            }
            if (isOnGauge && CoolTimeImg != null)
            {
                StartCoroutine(CoolDownAnimationGauge(initCoolTime));
            }
            if (isOnLight && SkillIconUILight != null)
            {
                StartCoroutine(CoolDownAnimationLight(initCoolTime));
            }
        }
        else
        {
            ResetSkillCoolDownUI();
        }
    }

    public void ResetSkillCoolDownUI()
    {
        StopAllCoroutines();
        ResetCoolTimeLight();
        ResetCoolTimeText();
        ResetCoolTimeGauge();
    }

    private IEnumerator CoolDownAnimationText(float initCoolTime)
    {
        Util.SetActive(CoolTimeText.gameObject, true);

        textTimer.Initialize(initCoolTime);
        while (textTimer.Tick())
        {
            float coolTime = Player.Instance.GetSkillCoolTime(skillName);
            CoolTimeText.text = Mathf.Ceil(coolTime).ToString();
            yield return null;
        }

        ResetCoolTimeText();
    }

    private IEnumerator CoolDownAnimationGauge(float initCoolTime)
    {
        gaugeTimer.Initialize(initCoolTime);
        while (gaugeTimer.Tick())
        {
            float coolTime = Player.Instance.GetSkillCoolTime(skillName);
            CoolTimeImg.fillAmount = 1 - coolTime / maxCoolTime;
            yield return null;
        }

        ResetCoolTimeGauge();
    }

    private IEnumerator CoolDownAnimationLight(float initCoolTime)
    {
        Util.SetActive(SkillIconUILight.gameObject, true);
        Util.SetRotationZ(SkillIconUILight.gameObject, 1 - initCoolTime / maxCoolTime);

        lightTimer.Initialize(initCoolTime);
        while (lightTimer.Tick())
        {
            float coolTime = Player.Instance.GetSkillCoolTime(skillName);
            Util.SetRotationZ(SkillIconUILight.gameObject, coolTime / maxCoolTime);
            yield return null;
        }

        ResetCoolTimeLight();
    }

    private void ResetCoolTimeLight()
    {
        if (SkillIconUILight != null)
        {
            Util.ResetRotationZ(SkillIconUILight.gameObject);
            Util.SetActive(SkillIconUILight.gameObject, false);
            lightTimer.SetRemainTimeZero();
        }
    }

    private void ResetCoolTimeText()
    {
        if (CoolTimeText != null)
        {
            Util.SetActive(CoolTimeText.gameObject, false);
            textTimer.SetRemainTimeZero();
        }
    }

    private void ResetCoolTimeGauge()
    {
        if (CoolTimeImg != null)
        {
            CoolTimeImg.fillAmount = 1;
            gaugeTimer.SetRemainTimeZero();
        }
    }


    public void UpdateSkillHotKey()
    {
        switch (skillName)
        {
            case SkillName.Attack:
                SkillHotKey.text = ScriptPool.KeyCodeText[KeySetting.GetKey(PlayerAction.Attack)];
                break;
            case SkillName.Mark:
                SkillHotKey.text = ScriptPool.KeyCodeText[KeySetting.GetKey(PlayerAction.Mark_Dash)];
                break;
            case SkillName.Dash:
                SkillHotKey.text = ScriptPool.KeyCodeText[KeySetting.GetKey(PlayerAction.Mark_Dash)];
                break;
            case SkillName.Skill1:
                SkillHotKey.text = ScriptPool.KeyCodeText[KeySetting.GetKey(PlayerAction.Skill1)];
                break;
            case SkillName.Skill2:
                SkillHotKey.text = ScriptPool.KeyCodeText[KeySetting.GetKey(PlayerAction.Skill2)];
                break;
        }
    }
}