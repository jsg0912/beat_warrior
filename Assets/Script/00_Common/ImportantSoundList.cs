using System.Collections.Generic;
using UnityEngine;
public class ImportantSoundList
{
    public List<AudioClip> importantSounds = new List<AudioClip>();

    public ImportantSoundList()
    {
        importantSounds.Add(SoundList.Instance.menuOpen);
        importantSounds.Add(SoundList.Instance.menuClose);
        importantSounds.Add(SoundList.Instance.altarOpen);
        importantSounds.Add(SoundList.Instance.altarClose);
        importantSounds.Add(SoundList.Instance.altarBuy);
        importantSounds.Add(SoundList.Instance.altarEquip);
        importantSounds.Add(SoundList.Instance.altarUnequip);

    }

    public bool IsImportantSound(AudioClip clip)
    {
        return importantSounds.Contains(clip);
    }
}
