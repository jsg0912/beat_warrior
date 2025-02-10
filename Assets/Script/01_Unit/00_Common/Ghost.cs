using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    public void ChangeSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void FlipSprite()
    {
        spriteRenderer.flipX = true;
    }
}