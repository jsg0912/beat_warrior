using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public int DialogNumber;

    Dictionary<int, string[]> DialogData;


    private void Awake()
    {
        DialogData = new Dictionary<int, string[]>();
        GenerateData();
    }

    private void GenerateData()
    {
        DialogData.Add(100, new string[] { "hello", "How are you" });
        DialogData.Add(200, new string[] { "hello", "How are you" });
    }

    public string GetDialog(int DialogNum, int DialogIndex)
    {
        return DialogData[DialogNum][DialogIndex];
    }

}
