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
    Hurt, // 체력 없는 상태: 특별한 처리를 위해 넣은 것으로 Player의 기본 Status랑은 관계 없음 - SDH, 20250210
    Happy, // 행복상태: 플레이어가 맵을 완료한 상태로 Player의 기본 Status랑은 관계 없음 - SDH, 20250210
    Dead,
    Null
}