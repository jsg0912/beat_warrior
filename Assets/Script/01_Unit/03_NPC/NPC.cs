using UnityEngine;

public class NPC : Monster
{
    // Start is called before the first frame update
    protected override void Start()
    {
        _animator = GetComponent<Animator>();
        monsterUnit = MonsterList.FindMonster(monsterName);

        // TODO: 하드코딩 수정
        pattern = new PatternNPCPriestGirl();
        pattern.Initialize(this);
    }

    // Update is called once per frame
    void Update()
    {
        pattern.PlayPattern();
    }
}
