using UnityEngine;
using System.Collections.Generic;

public class AltarPopup : PageAblePopupSystem
{
    [SerializeField] private List<TraitIcon> traitUIs;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Initialize();
    }

    public void Initialize()
    {
        AltarUIManager.Instance.UpdatePlayerSoulView();
        maxPage = Util.Round(AltarUIManager.Instance.salesSkillList.Length, traitUIs.Count);
        ChangePage(1);
    }

    public void ChangePage(int pageNumber)
    {
        currentPage = pageNumber;
        UpdatePage();
    }

    public void Refresh()
    {
        UpdatePage();
    }

    protected override void UpdatePage()
    {
        for (int i = 0; i < traitUIs.Count; i++)
        {
            int traitIndex = pageIndex * traitUIs.Count + i;
            if (traitIndex < AltarUIManager.Instance.salesSkillList.Length)
            {
                traitUIs[i].SetTraitName(AltarUIManager.Instance.salesSkillList[traitIndex]);
            }
            else
            {
                traitUIs[i].SetTraitName(SkillName.End);
            }
        }
    }

    public override bool TurnOnPopup()
    {
        bool success = base.TurnOnPopup();
        if (success)
        {
            Initialize();
            SoundManager.Instance.SFXPlay("AltarOpen", SoundList.Instance.altarOpen);
            Util.SetActive(MiniMap.Instance.gameObject, false);
            ShowCurrentPageText();
        }
        return success;
    }

    public override bool TurnOffPopup()
    {
        bool success = base.TurnOffPopup();
        if (success)
        {
            SoundManager.Instance.SFXPlay("AltarOpen", SoundList.Instance.altarClose);
            Util.SetActive(MiniMap.Instance.gameObject, true);
        }
        return success;
    }
}