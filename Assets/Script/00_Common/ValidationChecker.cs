// This class aims to check validation of our codes - SDH, 20250109
public static class ValidationChecker
{
    // When we release the project, then you have to comment out function call at GameManager - SDH, 20250109
    public static void Check()
    {
        TraitPriceList.CheckTraitPriceListValidation();
        MonsterList.CheckValidStat();
        ChapterInfo.CheckValid();
    }
}