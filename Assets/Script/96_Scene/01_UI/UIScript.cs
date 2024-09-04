using System.Collections.Generic;

public static class UIScript
{
    public static Dictionary<SkillName, Dictionary<Language, string>> TraitUIScript = new Dictionary<SkillName, Dictionary<Language, string>>
    {
        {SkillName.AppendMaxHP, new Dictionary<Language, string>{
            {
                Language.kr, "������ �� : �÷��̾��� �ִ� ü���� 1������Ų��."
            },{
                Language.en, "Strong Body: Increases the player's maximum stamina by 1."
            }}},
        {SkillName.SkillReset, new Dictionary<Language, string>{
            {
                Language.kr, "���� ����: ��ų ��� �� 10% Ȯ���� ����� ��ų ��Ÿ���� �ʱ�ȭ �ȴ�."
            },{
                Language.en, "Master of Sword: Skill cool time used with a 10% chance of using a skill will be initialized."

            }}},
        {SkillName.DoubleJump, new Dictionary<Language, string>{
            {
                Language.kr, "������ ��� : ���߿��� 1ȸ ������ �� �ִ�."
            },{
                Language.en, "Light movement: can take one leap in the air."
            }}},
        {SkillName.Execution, new Dictionary<Language, string>{
            {
                Language.kr, "��ī�ο� Į��: �ִ� ü���� 2 �̻��� ���� ü���� 1�� �Ǹ� ���� ó���Ѵ�."
            },{
                Language.en, "Sharp knife edge: Execute enemies when their maximum physical strength is 2 or higher."

            }}},
        {SkillName.AppendAttack, new Dictionary<Language, string>{
            {
                Language.kr, "����� �ܰ� : �⺻������ �ִ� �������� 1�����Ѵ�."
            },{
                Language.en, "Spare dagger: Maximum charge of base attack increases by 1."
            }}},
        {SkillName.KillRecoveryHP, new Dictionary<Language, string>{
            {
                Language.kr, "ü�� ���: 10���� óġ �� ü���� 1ȸ���Ѵ�."
            },{
                Language.en, "Stamina absorption: 1 recovery in stamina when you treat 10 animals."
            }}},
    };
}