using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveButton : MonoBehaviour
{
    public void OnClickSave()
    {
        SaveJSON newSave = new SaveJSON();

        newSave.saveRecord.soul = Inventory.Instance.GetSoulNumber();

        SaveLoadManager.instance.SaveData(newSave);
    }

    public void OnClickLoad()
    {
        SaveJSON loadedData = SaveLoadManager.instance.LoadMostRecentData();

        Debug.Log(loadedData.saveRecord.soul);
        Debug.Log(loadedData.saveSetting.resolutionWidth);
    }
}
