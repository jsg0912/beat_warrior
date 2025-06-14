using UnityEngine;

public class DamageCalculatorRandom : DamageCalculator
{
    public override int CalculateDamage(int defensePower, int baseAtk, int damageMultiplier = 1)
    {
        // Ensure that the damage is never negative
        int damage = baseAtk * damageMultiplier - defensePower;
        if (damage < 0) return 0;

        return Mathf.Max(damage + Random.Range(-1, 2), 1);
    }
}