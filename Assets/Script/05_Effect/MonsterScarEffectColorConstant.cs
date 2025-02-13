using System.Collections.Generic;
using UnityEngine;

public static class MonsterScarEffectColorConstant
{
    public static Dictionary<PlayerSkillEffectColor, Color> monsterScarEffectGlowColorInfo = new()
    {
        { PlayerSkillEffectColor.Yellow, new Color(255, 247, 97, 255) },
        { PlayerSkillEffectColor.Purple, new Color(179, 97, 255, 255) },
    };

    //TODO: Hit와 Particle이 다른 색깔로 정해져야하면 나눠야 함 - 신동환, 20250213
    public static Dictionary<PlayerSkillEffectColor, Color> monsterScarEffectHitNParticleColorInfo = new()
    {
        { PlayerSkillEffectColor.Yellow, new Color(255, 250, 152, 255) },
        { PlayerSkillEffectColor.Purple, new Color(207, 152, 255, 255) },
    };
}