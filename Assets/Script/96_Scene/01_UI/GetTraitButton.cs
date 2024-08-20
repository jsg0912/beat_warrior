using UnityEngine;
using UnityEngine.UI;

public class GetTraitButton : MonoBehaviour
{
    [SerializeField] private SkillName traitName;

    public void GetTrait()
    {
        Player.Instance.AddOrRemoveTrait(traitName);
    }
}
