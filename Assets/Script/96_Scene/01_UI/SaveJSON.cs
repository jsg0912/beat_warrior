using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveJSON
{
    public SaveRecord saveRecord;
    public SaveGamePlay saveGamePlay;
    public SaveSetting saveSetting;


    public SaveJSON()
    {
        SaveJSON previousData = LoadPreviousData();

        this.saveRecord = previousData?.saveRecord ?? new SaveRecord();
        this.saveGamePlay = previousData?.saveGamePlay ?? new SaveGamePlay();
        this.saveSetting = previousData?.saveSetting ?? new SaveSetting();
    }

    public SaveJSON(SaveRecord record)
    {
        SaveJSON previousData = LoadPreviousData();

        this.saveRecord = record;

        this.saveGamePlay = previousData?.saveGamePlay ?? new SaveGamePlay();
        this.saveSetting = previousData?.saveSetting ?? new SaveSetting();
    }

    public SaveJSON(SaveGamePlay gamePlay)
    {
        SaveJSON previousData = LoadPreviousData();

        this.saveGamePlay = gamePlay;

        this.saveRecord = previousData?.saveRecord ?? new SaveRecord();
        this.saveSetting = previousData?.saveSetting ?? new SaveSetting();
    }

    public SaveJSON(SaveSetting setting)
    {
        SaveJSON previousData = LoadPreviousData();

        this.saveSetting = setting;

        this.saveRecord = previousData?.saveRecord ?? new SaveRecord();
        this.saveGamePlay = previousData?.saveGamePlay ?? new SaveGamePlay();
    }


    private SaveJSON LoadPreviousData()
    {
        string path = Application.persistentDataPath + "/SaveJSON.save";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<SaveJSON>(json);
        }
        else
        {
            Debug.LogWarning("No previous data found.");
            return null;
        }
    }
}
