using System.Collections.Generic;

public class DialogScript
{
    public static Dictionary<CutSceneKind, Dictionary<Language, string[]>> CutSceneData = new()
    {
        {CutSceneKind.BeginningIntro, new ()
            {
                { Language.kr,new string[]{ "옛날에 한 고아 소녀가 있었습니다.", "소녀는 신성한 교단 XXX의 도움을 받았고,", "소녀는 교단에서 성기사로서의 수련을 받았지만, 암흑의 힘이 있었습니다.", "그러던 어느 날, 소녀는 교황의 부름을 받았습니다.", "교황: XX이여, XX 마을에서 이상한 소문이 돌고 있다. 확인하고 오너라.", "XX: 하이 고슈진 사마!"} },
                { Language.en,new string[]{ "Hello", "Nice to meet you"}}
            }
        }
    };

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
