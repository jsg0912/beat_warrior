using UnityEngine;

public static class ColliderCalculator
{
    public static Vector2 GetSizeBoxCollider2D(BoxCollider2D boxCollider2D)
    {
        return boxCollider2D.size;
    }

    public static Vector3 GetMiddlePosBoxCollider2D(BoxCollider2D boxCollider2D)
    {
        return boxCollider2D.bounds.center;
    }

    public static Vector3 GetBottomPosBoxCollider2D(BoxCollider2D boxCollider2D)
    {
        return boxCollider2D.bounds.center - new Vector3(0, boxCollider2D.size.y / 2, 0);
    }

    public static Vector2 GetSizePolygonCollider2D(PolygonCollider2D polygonCollider2D)
    {
        // 결과 반환
        return new Vector2(polygonCollider2D.bounds.extents.x * 2, polygonCollider2D.bounds.extents.y * 2);
    }

    public static Vector3 GetMiddlePosPolygonCollider2D(PolygonCollider2D polygonCollider2D)
    {
        return polygonCollider2D.bounds.center;
    }

    public static Vector3 GetBottomPosPolygonCollider2D(PolygonCollider2D polygonCollider2D)
    {
        return polygonCollider2D.bounds.center - new Vector3(0, polygonCollider2D.bounds.extents.y, 0);
    }
}
