public static class PatternFactory
{
    public static Pattern GetPatternByPatternName(PatternName patternName)
    {
        switch (patternName)
        {
            case PatternName.MeleeEnemy:
                return new MeleeEnemy();
            case PatternName.RangedMonster:
                return new RangedMonster();
        }

        throw new System.Exception($"{patternName} is not exist");
    }
}