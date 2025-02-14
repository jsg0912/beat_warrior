using System.Dynamic;

public static class PatternFactory
{
    public static Pattern GetPatternByPatternName(PatternName patternName)
    {
        switch (patternName)
        {
            case PatternName.Ibkkugi:
                return new PatternIbkkugi();
            case PatternName.Dulduli:
                return new PatternDulDulI();
            case PatternName.Itmomi:
                return new PatternItmomi();
            case PatternName.Ippali:
                return new PatternIppali();
            case PatternName.Koppulso:
                return new PatternKoppulso();
            case PatternName.Giljjugi:
                return new PatternGiljjugi();
            case PatternName.Ch2Boss:
                return new PatternCh2Boss();
        }

        throw new System.Exception($"{patternName} is not exist");
    }
}