using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public AltarPopup altarPopup;
    public MenuUI menuUI;
    public GameObject inGameUIPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void TurnOnAltarPopup()
    {
        altarPopup.TurnOnPopup();
    }

    public void SetInGameUIActive(bool active)
    {
        Util.SetActive(inGameUIPrefab, active);
    }
}