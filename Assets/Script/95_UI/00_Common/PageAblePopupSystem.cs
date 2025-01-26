using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class PageAblePopupSystem : PopupSystem
{
    [SerializeField] protected TMP_Text pageText;
    [SerializeField] protected Button prevButton;
    [SerializeField] protected Button nextButton;

    protected int maxPage;
    protected int currentPage = 1;

    public void OnPrevButtonClicked()
    {
        if (--currentPage < 1)
        {
            currentPage = 1;
        }

        if (SetCurrentPage(currentPage)) ChangePage(currentPage - 1);
    }

    public void OnNextButtonClicked()
    {
        if (++currentPage > maxPage)
        {
            currentPage = maxPage;
        }

        if (SetCurrentPage(currentPage)) ChangePage(currentPage - 1);
    }

    protected abstract void ChangePage(int index);

    public void SetMaxPage(int maxPage)
    {
        this.maxPage = maxPage;
    }

    private bool SetCurrentPage(int currentPage)
    {
        bool success = this.currentPage != currentPage;
        if (success)
        {
            this.currentPage = currentPage;
            ShowCurrentPageText();
        }
        return success;
    }

    private void ShowCurrentPageText()
    {
        pageText.text = currentPage + " / " + maxPage;
    }
}
