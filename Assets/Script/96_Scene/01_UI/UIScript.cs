// UI에서 언어 설정에 따라 보이는 글씨의 내용을 적는 곳 - 신동환, 2024.08
using System.Collections.Generic;

public static class UIScript
{
    public static Dictionary<SkillName, Dictionary<Language, string>> TraitUIScript = new Dictionary<SkillName, Dictionary<Language, string>>
    {
        {SkillName.Skill1, new Dictionary<Language, string>{
            {
                Language.kr, "Special Blade : 칼을 꺼내휘두르면서 넓은 범위에 데미지를 준다." +
                "\n \n 데미지 : 1 \n 충전횟수 : 1회 \n 쿨타임 : 8s"
            },{
                Language.en, "Special Blade: Take out a knife and rub it around to damage a wide range." +
                "\n \n Damage : 1 \n Charge Count : 1회 \n Cool Time : 8s"
            }}},
        {SkillName.Skill2, new Dictionary<Language, string>{
            {
                Language.kr, "Sweeping Blade : 플레이어 앞쪽으로 짧게 대쉬하며 적에데 데미지를 준다." +
                "\n \n 데미지 : 1 \n 충전횟수 : 1회 \n 쿨타임 : 8s"
            },{
                Language.en, "Sweeping Blade: Short dash to the front of the player and damage the enemy." +
                "\n \n Damage : 1 \n Charge Count : 1회 \n Cool Time : 8s"
            }}},
        {SkillName.Attack, new Dictionary<Language, string>{
            {
                Language.kr, "Holy Blade : 현재 플레이어가 바라보고 있는 방향으로 일정 범위로 공격을 한다. 적중 시 넉백 효과와 함께 데미지를 준다." +
                "\n \n 데미지 : 1 \n 충전횟수 : 2회 \n 쿨타임 : 4s"
            },{
                Language.en, "Holy Blade: Attack with a certain range in the direction the player is currently looking at. Damage with a knockback effect when hit." +
                "\n \n Damage : 1 \n Charge Count : 2회 \n Cool Time : 4s"
            }}},
        {SkillName.Mark, new Dictionary<Language, string>{
            {
                Language.kr, "표식 : 마우스 커서 방향으로 마법을 날린다. 마법을 맞은 적에게 표식이 남으며 대쉬를 사용할 수 있게한다." +
                "\n \n 충전횟수 : 1회 \n 쿨타임 : 8s"
            },{
                Language.en, "Table formula: I fly in the mouse cursor direction.It can be used to be able to use the magic." +
                "\n \n Charge Count : 1회 \n Cool Time : 8s"
            }}},
        {SkillName.Dash, new Dictionary<Language, string>{
            {
                Language.kr, "플레이어의 현재 위치에서 표식을 맞은 적의 뒤쪽으로 이동하며 플레이어와 표식이 맞은 적 사이의 모든 적에게 데미지를 준다." +
                "\n \n 데미지 : 1"
            },{
                Language.en, "Move from the player's current position to the back of the marked enemy and damage all enemies between the player and the marked enemy." +
                "\n \n Damage : 1"
            }}},
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