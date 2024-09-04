using System.Collections.Generic;

public static class UIScript
{
    public static Dictionary<SkillName, Dictionary<Language, string>> TraitUIScript = new Dictionary<SkillName, Dictionary<Language, string>>
    {
        {SkillName.AppendMaxHP, new Dictionary<Language, string>{
            {
                Language.kr, "강인한 몸 : 플레이어의 최대 체력을 1증가시킨다."
            },{
                Language.en, "Strong Body: Increases the player's maximum stamina by 1."
            }}},
        {SkillName.SkillReset, new Dictionary<Language, string>{
            {
                Language.kr, "검의 달인: 스킬 사용 시 10% 확률로 사용한 스킬 쿨타임이 초기화 된다."
            },{
                Language.en, "Master of Sword: Skill cool time used with a 10% chance of using a skill will be initialized."

            }}},
        {SkillName.DoubleJump, new Dictionary<Language, string>{
            {
                Language.kr, "가벼운 몸놀림 : 공중에서 1회 도약할 수 있다."
            },{
                Language.en, "Light movement: can take one leap in the air."
            }}},
        {SkillName.Execution, new Dictionary<Language, string>{
            {
                Language.kr, "날카로운 칼끝: 최대 체력이 2 이상인 적의 체력이 1이 되면 적을 처형한다."
            },{
                Language.en, "Sharp knife edge: Execute enemies when their maximum physical strength is 2 or higher."

            }}},
        {SkillName.AppendAttack, new Dictionary<Language, string>{
            {
                Language.kr, "예비용 단검 : 기본공격의 최대 충전량이 1증가한다."
            },{
                Language.en, "Spare dagger: Maximum charge of base attack increases by 1."
            }}},
        {SkillName.KillRecoveryHP, new Dictionary<Language, string>{
            {
                Language.kr, "체력 흡수: 10마리 처치 시 체력을 1회복한다."
            },{
                Language.en, "Stamina absorption: 1 recovery in stamina when you treat 10 animals."
            }}},
    };
}