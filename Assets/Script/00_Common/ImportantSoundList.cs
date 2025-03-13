using System.Collections.Generic;
using UnityEngine;

// TODO: 사용 방식이 바뀌어서 Naming 수정 필요
public class ImportantSoundList
{
    public List<AudioClip> importantSounds = new List<AudioClip>();
    public ImportantSoundList()
    {
        importantSounds.Add(SoundList.Instance.monsterHit);
        importantSounds.Add(SoundList.Instance.defMonsterHit);
        importantSounds.Add(SoundList.Instance.monsterIppaliAttack);
        importantSounds.Add(SoundList.Instance.monsterIbkkugiAttack);
        importantSounds.Add(SoundList.Instance.monsterKoppulsoCharge);
        importantSounds.Add(SoundList.Instance.monsterKoppulsoAttack);
        importantSounds.Add(SoundList.Instance.monsterDulduliCharge);
        importantSounds.Add(SoundList.Instance.monsterDulduliAttack);
        importantSounds.Add(SoundList.Instance.monsterGiljjugiAttack);
        importantSounds.Add(SoundList.Instance.monsterItmomiAttack);
        importantSounds.Add(SoundList.Instance.monsterItmomiThorn);
    }

    public bool IsDuplicateSound(AudioClip clip)
    {
        return importantSounds.Contains(clip);
    }
}
