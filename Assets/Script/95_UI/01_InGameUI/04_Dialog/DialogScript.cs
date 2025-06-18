using System.Collections.Generic;

public class DialogScript
{
    public static Dictionary<CutSceneKind, Dictionary<Language, string[]>> CutSceneData = new()
    {
        {CutSceneKind.BeginningIntro, new ()
            {
                { Language.kr, new string[]{
                    "옛날, 세상으로부터 버려진 노아라는 소녀가 있었습니다.",
                    "노아는 신성한 교단 테노브라에 의해 구원을 받았고,",
                    "그곳에서 성기사의 길을 걷게 되었지만, 그녀 안엔 어둠의 기운이 자라나고 있었습니다.",
                    "그러던 어느 날, 노아는 교황에게 불려갔습니다.",
                    "교황: 노아여, 베르나스 마을에서 불길한 소문이 돌고 있다. 그 진위를 네가 확인하고 오너라.\r노아: …명 받들겠습니다. 곧 출발하겠습니다.",
                    "노아: …이곳이 이렇게까지 무너졌을 줄은 몰랐군.\r노아: 표면적으로는 이 안에서 무슨 일이 벌어졌는지 알 수 없어.\r노아: …더 깊은 곳까지 들어가보자." } },
                { Language.en, new string[]{
                    "Once upon a time, there was a girl named Noah who was abandoned by the world.",
                    "Noah was saved by the holy order of Tenobra,",
                    "and there she walked the path of a holy knight, but a dark power was growing within her.",
                    "One day, Noah was summoned by the Pope.",
                    "Pope: Noah, ominous rumors are spreading from the village of Vernas. Go and verify their truth.\rNoah: ...I will obey. I shall depart at once.",
                    "Noah: ...I didn't expect this place to be so ruined.\rNoah: I can't tell what happened here just from the surface.\rNoah: ...Let's go deeper."
                }}
            }
        }
    };

    public static Dictionary<DialogName, Dictionary<Language, (DialogSpeaker, string[])[]>> DialogData = new()
    {
        {DialogName.Tutorial, new ()
        {
            {
                Language.kr, new []{
                    (DialogSpeaker.Sister, new string[]{ "노아, 이제 떠날 준비는 되셨나요?" }),
                    (DialogSpeaker.Noa, new string[]{ "준비란 게… 늘 되는 건 아니죠. 그래도 가야 하니까요." }),
                    (DialogSpeaker.Sister, new string[]{ "빈민가로 가주세요. 최근 들어, 그곳에서 이상한 기척이 느껴진다고 합니다." }),
                    (DialogSpeaker.Noa, new string[]{ "…오랜만이네요, 그쪽은." }),
                    (DialogSpeaker.Sister, new string[]{ "당신에게 익숙한 곳이기에... 더 많은 것을 느낄 수 있을지도 모릅니다." }),
                    (DialogSpeaker.Noa, new string[]{ "무엇을 보든, 피하지 않겠습니다. 다녀오죠." })
                }
            },
            {
                Language.en, new []{
                    (DialogSpeaker.Sister, new string[]{ "Noah, are you ready to leave now?" }),
                    (DialogSpeaker.Noa, new string[]{ "Being ready... isn't always possible. But I have to go anyway." }),
                    (DialogSpeaker.Sister, new string[]{ "Please go to the slums. Recently, there have been reports of strange presences there." }),
                    (DialogSpeaker.Noa, new string[]{ "...It's been a while since I've been there." }),
                    (DialogSpeaker.Sister, new string[]{ "Since it's a place familiar to you... you might be able to sense more." }),
                    (DialogSpeaker.Noa, new string[]{ "Whatever I see, I won't run from it. I'll be back." })
                }
            }
        }},
        {DialogName.Act1, new ()
        {
            {
                Language.kr, new []{
                    (DialogSpeaker.Noa, new string[]{
                        "…기억보다 더 황폐하군.",
                        "사람 냄새보다 썩은 냄새가 먼저 코를 찌르다니.",
                        "여긴... 오랫동안 버려져 있었던 거겠지.",
                        "오래도록 방치된 건… 건물만이 아니겠지."
                    })
                }
            },
            {
                Language.en, new []{
                    (DialogSpeaker.Noa, new string[]{
                        "...It's more desolate than I remember.",
                        "The stench of decay hits before any trace of human presence.",
                        "This place... must have been abandoned for a long time.",
                        "It's not just the buildings that have been neglected for so long..."
                    })
                }
            }
        }},
        {DialogName.Act2, new ()
        {
            {
                Language.kr, new []{
                    (DialogSpeaker.Noa, new string[]{
                        "…이곳이 이렇게까지 무너졌을 줄은 몰랐군.",
                        "기도가 울려 퍼지던 자리에… 이제는 낯선 기척만 가득해.",
                        "표면만으로는 이 안에서 무슨 일이 벌어졌는지 알 수 없어.",
                        "…더 깊은 곳까지 들어가보자."
                    })
                }
            },
            {
                Language.en, new []{
                    (DialogSpeaker.Noa, new string[]{
                        "...I didn't expect this place to be so ruined.",
                        "Where prayers once echoed... now only strange presences fill the space.",
                        "I can't tell what happened here just from the surface.",
                        "...Let's go deeper."
                    })
                }
            }
        }}
    };
}
