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
        if (faceImage.sprite != idleFace)
            faceImage.sprite = idleFace;
    }

    public void SetHurtFace()
    {
        if (faceImage.sprite != hurtFace)
            faceImage.sprite = hurtFace;
    }

    public void SetHappyFace()
    {
        if (Random.Range(0, 10) == 0) SetBeautyFace();
        else
        {
            if (faceImage.sprite != happyFace)
                faceImage.sprite = happyFace;
        }
    }

    public void SetBeautyFace()
    {
        if (faceImage.sprite != beautyFace)
            faceImage.sprite = beautyFace;
    }

    public void SetCrazyFace()
    {
        if (faceImage.sprite != crazyFace)
            faceImage.sprite = crazyFace;
    }
}