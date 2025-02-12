// UI에서 언어 설정에 따라 보이는 글씨의 내용을 적는 곳 - 신동환, 2024.08
using System.Collections.Generic;
using UnityEngine;

public static class ScriptPool
{
    public static Dictionary<SkillName, Dictionary<Language, string>> TraitNameScript = new Dictionary<SkillName, Dictionary<Language, string>>
    {
        {SkillName.Skill1, new Dictionary<Language, string>{
            {
                Language.kr, "Special Blade"
            },{
                Language.en, "Special Blade"
            }}},
        {SkillName.Skill2, new Dictionary<Language, string>{
            {
                Language.kr, "Sweeping Blade"
            },{
                Language.en, "Sweeping Blade"
            }}},
        {SkillName.Attack, new Dictionary<Language, string>{
            {
                Language.kr, "신성한 검"
            },{
                Language.en, "Holy Blade"
            }}},
        {SkillName.Mark, new Dictionary<Language, string>{
            {
                Language.kr, "표식"
            },{
                Language.en, "Mark"
            }}},
        {SkillName.Dash, new Dictionary<Language, string>{
            {
                Language.kr, "대쉬"
            },{
                Language.en, "Dash"
            }}},
        {SkillName.AppendMaxHP, new Dictionary<Language, string>{
            {
                Language.kr, "강인한 몸"
            },{
                Language.en, "Strong Body"
            }}},
        {SkillName.SkillReset, new Dictionary<Language, string>{
            {
                Language.kr, "검의 달인"
            },{
                Language.en, "Master of Sword"

            }}},
        {SkillName.DoubleJump, new Dictionary<Language, string>{
            {
                Language.kr, "가벼운 몸놀림"
            },{
                Language.en, "Light movement"
            }}},
        {SkillName.Execution, new Dictionary<Language, string>{
            {
                Language.kr, "날카로운 칼끝"
            },{
                Language.en, "The Edge of Knife"

            }}},
        {SkillName.AppendAttack, new Dictionary<Language, string>{
            {
                Language.kr, "검술 수련"
            },{
                Language.en, "Spare Dagger"
            }}},
        {SkillName.KillRecoveryHP, new Dictionary<Language, string>{
            {
                Language.kr, "체력 흡수"
            },{
                Language.en, "Health Absorption"
            }}},
        {SkillName.Revive, new Dictionary<Language, string>{
            {
                Language.kr, "여신의 은총"
            },{
                Language.en, "Grace of Goddess"
            }}},
    };

    public static Dictionary<SkillName, Dictionary<Language, string>> TraitUIScript = new Dictionary<SkillName, Dictionary<Language, string>>
    {
        {SkillName.Skill1, new Dictionary<Language, string>{
            {
                Language.kr, "칼을 꺼내휘두르면서 넓은 범위에 데미지를 준다." +
                "\n \n 데미지 : 1 \n 충전횟수 : 1회 \n 쿨타임 : 8s"
            },{
                Language.en, "Take out a knife and rub it around to damage a wide range." +
                "\n \n Damage : 1 \n Charge Count : 1회 \n Cool Time : 8s"
            }}},
        {SkillName.Skill2, new Dictionary<Language, string>{
            {
                Language.kr, "플레이어 앞쪽으로 짧게 대쉬하며 적에데 데미지를 준다." +
                "\n \n 데미지 : 1 \n 충전횟수 : 1회 \n 쿨타임 : 8s"
            },{
                Language.en, "Short dash to the front of the player and damage the enemy." +
                "\n \n Damage : 1 \n Charge Count : 1회 \n Cool Time : 8s"
            }}},
        {SkillName.Attack, new Dictionary<Language, string>{
            {
                Language.kr, "현재 플레이어가 바라보고 있는 방향으로 일정 범위로 공격을 한다. 적중 시 넉백 효과와 함께 데미지를 준다." +
                "\n \n 데미지 : 1 \n 충전횟수 : 2회 \n 쿨타임 : 4s"
            },{
                Language.en, "Attack with a certain range in the direction the player is currently looking at. Damage with a knockback effect when hit." +
                "\n \n Damage : 1 \n Charge Count : 2회 \n Cool Time : 4s"
            }}},
        {SkillName.Mark, new Dictionary<Language, string>{
            {
                Language.kr, "마우스 커서 방향으로 마법을 날린다. 마법을 맞은 적에게 표식이 남으며 대쉬를 사용할 수 있게한다." +
                "\n \n 충전횟수 : 1회 \n 쿨타임 : 8s"
            },{
                Language.en, "I fly in the mouse cursor direction.It can be used to be able to use the magic." +
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
                Language.kr, "플레이어의 최대 체력을 1증가시킨다."
            },{
                Language.en, "Increases the player's maximum stamina by 1."
            }}},
        {SkillName.SkillReset, new Dictionary<Language, string>{
            {
                Language.kr, "스킬 사용 시 10% 확률로 사용한 스킬 쿨타임이 초기화 된다."
            },{
                Language.en, "Skill cool time used with a 10% chance of using a skill will be initialized."

            }}},
        {SkillName.DoubleJump, new Dictionary<Language, string>{
            {
                Language.kr, "공중에서 1회 도약할 수 있다."
            },{
                Language.en, "can take one leap in the air."
            }}},
        {SkillName.Execution, new Dictionary<Language, string>{
            {
                Language.kr, "최대 체력이 2 이상인 적의 체력이 1이 되면 적을 처형한다."
            },{
                Language.en, "Execute enemies when their maximum physical strength is 2 or higher."

            }}},
        {SkillName.AppendAttack, new Dictionary<Language, string>{
            {
                Language.kr, "기본공격의 최대 충전량이 1증가한다."
            },{
                Language.en, "Maximum charge of base attack increases by 1."
            }}},
        {SkillName.KillRecoveryHP, new Dictionary<Language, string>{
            {
                Language.kr, "10마리 처치 시 체력을 1회복한다."
            },{
                Language.en, "1 recovery in stamina when you treat 10 animals."
            }}},
        {SkillName.Revive, new Dictionary<Language, string>{
            {
                Language.kr, "체력이 모두 줄어 사망했을 때 한 번만 체력 2로 살아난다. (1회용)"
            },{
                Language.en, "When the stamina is completely reduced and died, it is revived with stamina 2 only once. (1 time use)"
            }}},
    };

    public static Dictionary<SettingContentIndex, Dictionary<Language, string>> SettingsContentTitles = new Dictionary<SettingContentIndex, Dictionary<Language, string>>
    {
        {SettingContentIndex.KeySetting, new Dictionary<Language, string>{
            { Language.kr, "키설정"},
            { Language.en, "Key Setting"},
        }},
        {SettingContentIndex.ScreenSetting, new Dictionary<Language, string>{
            { Language.kr, "화면"},
            { Language.en, "Screen"},
         } },
        {SettingContentIndex.SoundSetting, new Dictionary<Language, string>{
            { Language.kr, "사운드"},
            { Language.en, "Sound"},
        } },
    };

    public static Dictionary<SyetemMassageType, Dictionary<Language, string>> SystemMassageDictionary = new Dictionary<SyetemMassageType, Dictionary<Language, string>>
    {
        {SyetemMassageType.EnemyRemaining,new Dictionary<Language, string>
        {
            {Language.kr, "남아있는 적이 있습니다" },
            {Language.en, "Monsters still remain" },
        }},
    };

    public static Dictionary<KeyCode, string> KeyCodeText = new()
    {
        { KeyCode.A, "A"},
        { KeyCode.B, "B"},
        { KeyCode.C, "C"},
        { KeyCode.D, "D"},
        { KeyCode.E, "E"},
        { KeyCode.F, "F"},
        { KeyCode.G, "G"},
        { KeyCode.H, "H"},
        { KeyCode.I, "I"},
        { KeyCode.J, "J"},
        { KeyCode.K, "K"},
        { KeyCode.L, "L"},
        { KeyCode.M, "M"},
        { KeyCode.N, "N"},
        { KeyCode.O, "O"},
        { KeyCode.P, "P"},
        { KeyCode.Q, "Q"},
        { KeyCode.R, "R"},
        { KeyCode.S, "S"},
        { KeyCode.T, "T"},
        { KeyCode.U, "U"},
        { KeyCode.V, "V"},
        { KeyCode.W, "W"},
        { KeyCode.X, "X"},
        { KeyCode.Y, "Y"},
        { KeyCode.Z, "Z"},
        { KeyCode.Alpha0, "0"},
        { KeyCode.Alpha1, "1"},
        { KeyCode.Alpha2, "2"},
        { KeyCode.Alpha3, "3"},
        { KeyCode.Alpha4, "4"},
        { KeyCode.Alpha5, "5"},
        { KeyCode.Alpha6, "6"},
        { KeyCode.Alpha7, "7"},
        { KeyCode.Alpha8, "8"},
        { KeyCode.Alpha9, "9"},
        { KeyCode.F1, "F1"},
        { KeyCode.F2, "F2"},
        { KeyCode.F3, "F3"},
        { KeyCode.F4, "F4"},
        { KeyCode.F5, "F5"},
        { KeyCode.F6, "F6"},
        { KeyCode.F7, "F7"},
        { KeyCode.F8, "F8"},
        { KeyCode.F9, "F9"},
        { KeyCode.F10, "F10"},
        { KeyCode.F11, "F11"},
        { KeyCode.F12, "F12"},
        { KeyCode.Escape, "Esc"},
        { KeyCode.Return, "Enter"},
        { KeyCode.Backspace, "BS"},
        { KeyCode.Tab, "Tab"},
        { KeyCode.LeftArrow, "←"},
        { KeyCode.RightArrow, "→"},
        { KeyCode.UpArrow, "↑"},
        { KeyCode.DownArrow, "↓"},
        { KeyCode.LeftShift, "L-Shift"},
        { KeyCode.RightShift, "R-Shift"},
        { KeyCode.LeftControl, "L-Ctrl"},
        { KeyCode.RightControl, "R-Ctrl" },
        { KeyCode.LeftAlt, "L-Alt"},
        { KeyCode.RightAlt, "R-Alt"},
        { KeyCode.LeftCommand, "L-Cmd"},
        { KeyCode.RightCommand, "R-Cmd"},
        { KeyCode.Mouse0, "MBL"},
        { KeyCode.Mouse1, "MBR"},
        { KeyCode.Mouse2, "MBM"},
        { KeyCode.Mouse3, "MB4"},
        { KeyCode.Mouse4, "MB5"},
        { KeyCode.Mouse5, "MB6"},
        { KeyCode.Mouse6, "MB7"},
        { KeyCode.PageDown, "PageDown"},
        { KeyCode.PageUp, "PageUp"},
        { KeyCode.Home, "Home"},
        { KeyCode.End, "End"},
        { KeyCode.Insert, "Insert"},
        { KeyCode.Delete, "Delete"},
        { KeyCode.CapsLock, "CapsLock"},
        { KeyCode.ScrollLock, "ScrollLock"},
        { KeyCode.Pause, "Pause"},
        { KeyCode.Print, "Print"},
        { KeyCode.SysReq, "SysReq"},
        { KeyCode.Break, "Break"},
        { KeyCode.Menu, "Menu"},
        { KeyCode.Numlock, "NumLock"},
        { KeyCode.Keypad0, "KP0"},
        { KeyCode.Keypad1, "KP1"},
        { KeyCode.Keypad2, "KP2"},
        { KeyCode.Keypad3, "KP3"},
        { KeyCode.Keypad4, "KP4"},
        { KeyCode.Keypad5, "KP5"},
        { KeyCode.Keypad6, "KP6"},
        { KeyCode.Keypad7, "KP7"},
        { KeyCode.Keypad8, "KP8"},
        { KeyCode.Keypad9, "KP9"},
        { KeyCode.KeypadDivide, "KP/"},
        { KeyCode.KeypadMultiply, "KP*"},
        { KeyCode.KeypadMinus, "KP-"},
        { KeyCode.KeypadPlus, "KP+"},
        { KeyCode.KeypadEnter, "KP-Enter"},
        { KeyCode.KeypadPeriod, "KP."},
        { KeyCode.KeypadEquals, "KP="},
        { KeyCode.Comma, "KP," }
    };
}