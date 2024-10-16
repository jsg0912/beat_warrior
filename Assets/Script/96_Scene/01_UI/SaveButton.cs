using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveButton : MonoBehaviour
{
    public void OnClickSave()
    {
        SkillName[] mySkill = { SkillName.KillRecoveryHP, SkillName.AppendAttack};
        //SaveJSON newSave = new SaveJSON;
        //SaveLoadManager.instance.SaveData(newSave);
    }

    public void OnClickLoad()
    {
        SaveJSON loadedData = SaveLoadManager.instance.LoadMostRecentData();

        //Debug.Log(loadedData.soul);
        //Debug.Log(loadedData.chapterName);
        //Debug.Log(loadedData.resolutionWidth);
    }
}
