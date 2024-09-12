using System.Dynamic;

public static class PatternFactory
{
    public static Pattern GetPatternByPatternName(PatternName patternName)
    {
        switch (patternName)
        {
            case PatternName.MeleeEnemy:
                return new MeleeEnemy();
            case PatternName.Monster2:
                return new Monster2Pattern();
        }

        throw new System.Exception($"{patternName} is not exist");
    }
}