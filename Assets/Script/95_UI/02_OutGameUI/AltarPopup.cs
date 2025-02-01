using UnityEngine;
using System.Collections.Generic;

public class AltarPopup : PageAblePopupSystem
{
    [SerializeField] private List<TraitUI> traitUIs;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Initialize();
    }

    public void Initialize()
    {
        ChangePage(1);
        AltarUIManager.Instance.UpdatePlayerSoulView();
        maxPage = Util.Round(AltarUIManager.Instance.salesSkillList.Length, traitUIs.Count);
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
                traitUIs[i].SetSkillName(AltarUIManager.Instance.salesSkillList[traitIndex]);
            }
            else
            {
                traitUIs[i].SetSkillName(SkillName.End);
            }
        }
    }

    public override bool TurnOnPopup()
    {
        bool success = base.TurnOnPopup();
        if (success)
        {
            Initialize();
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
            Util.SetActive(MiniMap.Instance.gameObject, true);
        }
        return success;
    }
}