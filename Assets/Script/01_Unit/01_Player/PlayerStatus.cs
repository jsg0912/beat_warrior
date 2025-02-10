// Caution, When you add new status or delete status, you must check whether there is a problem in the player animation
// Caution, If you want to add new status, you must put the new status under the "Dead"

public enum PlayerStatus
{
    Normal,
    Skill,
    Dash,
    Unmovable,
    Unattackable,
    Stun, // Unmovable + Unattackable
    Hurt, // 체력 없는 상태: 특별한 처리를 위해 넣은 것으로 게임 시스템과는 관계없음 - SDH, 20250210
    Rest, // 휴식 중: 특별한 처리를 위해 넣은 것으로 게임 시스템과는 관계없음 - SDH, 20250210
    Dead,
    Null
}