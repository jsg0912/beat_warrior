using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogScript
{
    public static Dictionary<DialogName, Dictionary<Language, string[]>> DialogData = new Dictionary<DialogName, Dictionary<Language, string[]>>
    {
        {DialogName.Act1,new Dictionary<Language, string[]>
        {
            { Language.kr,new string[]{ "�ȳ��ϼ���", "�ݰ�����"} },
            { Language.en,new string[]{ "Hello", "Nice to meet you"}}
        }},
        {DialogName.Act2,new Dictionary<Language, string[]>
        {
            { Language.kr,new string[]{ "�ȳ��ϼ���!", "�ݰ�����!"}},
            { Language.en,new string[]{ "Hello!","Nice to meet you!"}}
        }}
    };
}
