using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public DialogName dialogName;

    Dictionary<DialogName, string[]> DialogData;


    private void Awake()
    {
        DialogData = new Dictionary<DialogName, string[]>();
        GenerateData();
    }

    private void GenerateData()
    {
        DialogData.Add(DialogName.Act1, new string[] { "hello", "How are you" });
        DialogData.Add(DialogName.Act2, new string[] { "hello", "How are you" });
    }

    public string GetDialog(DialogName dialogName, int DialogIndex)
    {
        if (DialogIndex == DialogData[dialogName].Length)
        {
            return null;
        }
        else
        {
            return DialogData[dialogName][DialogIndex];
        }
    }

}