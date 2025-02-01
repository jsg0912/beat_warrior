using UnityEngine;

public class PlayerHPUI : MonoBehaviour
{
    [SerializeField] private GameObject heart;
    [SerializeField] private GameObject empty;

    public void ShowHP(bool isFilled)
    {
        Util.SetActive(heart, isFilled);
        Util.SetActive(empty, !isFilled);
    }
}