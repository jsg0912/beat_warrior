public enum StatKind
{
    HP,
    ATK,
    Def,
    Necessary, // 이를 기준으로 위는 필수, 아래는 필수 아님 - 신동환, 2024.08.14
    JumpCount,
    AttackCount,
    Null
}