using UnityEngine;

public class DamageCalculatorFix : DamageCalculator
{
    public override int CalculateDamage(int defensePower, int baseAtk, int damageMultiplier = 1)
    {
        // Ensure that the damage is never negative
        int damage = baseAtk + damageMultiplier - defensePower;
        return Mathf.Max(damage, 0);
    }
}