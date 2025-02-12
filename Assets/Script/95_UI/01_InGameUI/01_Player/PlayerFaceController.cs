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
    private PlayerStatus currentFace;

    public void SetPlayerFace(PlayerStatus playerStatus, int hp)
    {
        if (playerStatus == PlayerStatus.Happy) SetHappyFace(); // Stage Clear is the most valuable face
        else
        {
            if (hp <= PlayerConstant.PlayerHurtFaceTriggerHp) SetHurtFace();
            else SetIdleFace();
        }
    }

    private bool IsSameFace(PlayerStatus newFace)
    {
        if (currentFace == newFace) return true;
        currentFace = newFace;
        return false;
    }

    public void SetIdleFace()
    {
        if (IsSameFace(PlayerStatus.Normal)) return;

        if (Util.RandomBool(10)) SetBeautyFace(); // 임시로 10% 확률로 이쁜 얼굴
        else faceImage.sprite = idleFace;
    }

    public void SetHurtFace()
    {
        if (IsSameFace(PlayerStatus.Hurt)) return;

        if (faceImage.sprite != hurtFace)
            faceImage.sprite = hurtFace;
    }

    public void SetHappyFace()
    {
        if (IsSameFace(PlayerStatus.Happy)) return;

        if (faceImage.sprite != happyFace)
            faceImage.sprite = happyFace;
    }


    // Below faces are for fun...
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