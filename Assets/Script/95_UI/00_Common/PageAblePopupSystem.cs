using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class PageAblePopupSystem : PopupSystem
{
    [SerializeField] protected TMP_Text pageIndexView;
    [SerializeField] protected Button prevButton;
    [SerializeField] protected Button nextButton;
    [SerializeField] protected TMP_Dropdown sortOptions; // TODO: 기능구현해야함 - 신동환, 2025.01.27

    protected int maxPage;
    protected int currentPage = 1;
    protected int pageIndex => currentPage - 1;

    protected virtual void Start()
    {
        ShowCurrentPageText();
    }

    public void OnPrevButtonClicked()
    {
        if (--currentPage < 1)
        {
            currentPage = 1;
        }

        if (SetCurrentPage(currentPage)) UpdatePage();
    }

    public void OnNextButtonClicked()
    {
        if (++currentPage > maxPage)
        {
            currentPage = maxPage;
        }

        if (SetCurrentPage(currentPage)) UpdatePage();
    }

    public void SetMaxPage(int maxPage)
    {
        this.maxPage = maxPage;
    }

    protected bool SetCurrentPage(int currentPage)
    {
        bool success = this.currentPage != currentPage;
        if (success)
        {
            this.currentPage = currentPage;
            ShowCurrentPageText();
        }
        return success;
    }

    protected void ShowCurrentPageText()
    {
        pageIndexView.text = currentPage + " / " + maxPage;
    }

    protected abstract void UpdatePage();
}
