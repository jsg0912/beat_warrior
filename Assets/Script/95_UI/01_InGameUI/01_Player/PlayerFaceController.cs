using UnityEngine;
using UnityEngine.UI;

public class PlayerFaceController : MonoBehaviour
{
    [SerializeField] private Image faceImage;
    [SerializeField] private Sprite idleFace;
    [SerializeField] private Sprite hurtFace;
    [SerializeField] private Sprite happyFace;
    [SerializeField] private Sprite crazyFace;
    [SerializeField] private Sprite beautyFace;

    public void SetIdleFace()
    {
        if (Random.Range(0, 100) == 0) faceImage.sprite = beautyFace;
        else faceImage.sprite = idleFace;

    }

    public void SetHurtFace()
    {
        DebugConsole.Log("SetHurtFace");
        faceImage.sprite = hurtFace;
    }

    public void SetHappyFace()
    {
        DebugConsole.Log("SetHappyFace");
        faceImage.sprite = happyFace;
    }

    public void SetCrazyFace()
    {
        DebugConsole.Log("SetCrazyFace");
        faceImage.sprite = crazyFace;
    }
}