public abstract class DamageCalculator
{
    // TODO: Skill에서 Multiplier 받아서 계산하도록 수정해야함(현재 Skill에서 직접 곱하고 그 값을 Collider에 저장해서 이 함수를 타고 있음) - SDH, 20250614
    public abstract int CalculateDamage(int defensePower, int baseAtk, int damageMultiplier = 1);
}