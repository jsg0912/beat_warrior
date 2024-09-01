using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AltarUI : MonoBehaviour
{
    [SerializeField] private GameObject Altar;
    [SerializeField] private TextMeshProUGUI SpiritText;

    private bool isAltarActive = false;

    public void SetAltarActive()
    {
        isAltarActive = !isAltarActive;
        Altar.SetActive(isAltarActive);

        SpiritText.text = Inventory.Instance.GetSpiritNumber().ToString();
    }
}
