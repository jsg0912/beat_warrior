// 모든 물리적 오브젝트에 붙을 Class
using UnityEngine;

public class CommonObject
{
    protected Sprite sprtie;
    private bool isCollision;

    protected void SetIsCollision(bool isCollision )
    {
        this.isCollision = isCollision;
    }

    public bool GetIsCollision()
    {
        return isCollision;
    }
}
