using System;
using System.IO;
using UnityEngine;
using System.Linq;

public class SaveLoadManager : MonoBehaviour
{
    public int maxSaveFiles = 5;

    public static SaveLoadManager instance;

    public void SaveData(SaveJSON data)
    {
        string json = JsonUtility.ToJson(data);

        string timestamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string fileName = "SaveJSON_" + timestamp + ".save";

        string path = Application.persistentDataPath + "/" + fileName;

        File.WriteAllText(path, json);

        Debug.Log("Data saved to: " + path);

        ManageSaveFiles();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject)
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void ManageSaveFiles()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] files = dir.GetFiles("*.save").OrderBy(f => f.CreationTime).ToArray();

        if (files.Length > maxSaveFiles)
        {
            for (int i = 0; i < files.Length - maxSaveFiles; i++)
            {
                Debug.Log("Deleting old save file: " + files[i].Name);
                files[i].Delete();
            }
        }
    }

    public SaveJSON LoadData(string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName;

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveJSON data = JsonUtility.FromJson<SaveJSON>(json);
            return data;
        }
        else
        {
            Debug.LogWarning("Save file not found at: " + path);
            return null;
        }
    }

    public SaveJSON LoadMostRecentData()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] files = dir.GetFiles("*.save").OrderByDescending(f => f.CreationTime).ToArray();

        if (files.Length == 0)
        {
            Debug.LogWarning("No save files found.");
            return null;
        }

        foreach (var file in files)
        {
            try
            {
                string json = File.ReadAllText(file.FullName);
                SaveJSON data = JsonUtility.FromJson<SaveJSON>(json);
                Debug.Log("Data successfully loaded from: " + file.Name);
                return data;
            }
            catch (Exception e)
            {
                Debug.LogWarning("Failed to load file: " + file.Name + ". Error: " + e.Message);
            }
        }

        Debug.LogError("All save files are corrupted or failed to load.");
        return null;
    }
}
