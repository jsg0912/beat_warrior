using UnityEngine;

public class CommonObject : MonoBehaviour
{
    protected Sprite sprtie;
    private bool isCollision;

    protected void SetIsCollision(bool isCollision)
    {
        this.isCollision = isCollision;
    }

    public bool GetIsCollision()
    {
        return isCollision;
    }
}
