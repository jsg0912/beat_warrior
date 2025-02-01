using System.Dynamic;

public static class PatternFactory
{
    public static Pattern GetPatternByPatternName(PatternName patternName)
    {
        switch (patternName)
        {
            case PatternName.Monster2:
                return new Monster2Pattern();
            case PatternName.Monster3:
                return new Monster3Pattern();
            case PatternName.RangedMonster:
                return new PatternIbkkugi();
            // TODO: 이 위는 모두 임시임 - SDH, 20250131
            case PatternName.Ibkkugi:
                return new PatternIbkkugi();
            case PatternName.Jiljili:
                return new PatternDulDulI();
            case PatternName.Itmomi:
                return new PatternItmomi();
            case PatternName.Ippali:
                return new PatternIppali();
            case PatternName.Koppulso:
                return new PatternKoppulso();
            case PatternName.Giljjugi:
                return new PatternGiljjugi();
        }

        throw new System.Exception($"{patternName} is not exist");
    }
}