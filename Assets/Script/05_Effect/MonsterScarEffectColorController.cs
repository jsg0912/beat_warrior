using UnityEngine;

public class MonsterScarEffectColorController : MonoBehaviour
{
    [SerializeField] private ParticleSystem glowEffect;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private ParticleSystem particleEffect;
    private PlayerSkillEffectColor lastColor = PlayerSkillEffectColor.Yellow;

    public void OnEnable()
    {
        PlayerSkillEffectColor playerSkillColor = Player.Instance.playerSkillEffectColor;
        if (lastColor != playerSkillColor)
        {
            lastColor = playerSkillColor;
            var glowMain = glowEffect.main;
            glowMain.startColor = MonsterScarEffectColorConstant.monsterScarEffectGlowColorInfo[playerSkillColor];
            var hitMain = hitEffect.main;
            var particleMain = particleEffect.main;
            Color newColor = MonsterScarEffectColorConstant.monsterScarEffectHitNParticleColorInfo[playerSkillColor];
            hitMain.startColor = newColor;
            particleMain.startColor = newColor;
        }
    }
}
