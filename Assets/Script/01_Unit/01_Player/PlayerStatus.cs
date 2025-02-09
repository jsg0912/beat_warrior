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
    Dead,
    Null
}