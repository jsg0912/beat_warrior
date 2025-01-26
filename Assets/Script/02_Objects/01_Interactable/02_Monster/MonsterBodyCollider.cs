using UnityEngine;

public class MonsterBodyCollider : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private PolygonCollider2D polygonCollider;

    public void Start()
    {
        boxCollider = GetComponentInChildren<BoxCollider2D>();
        polygonCollider = GetComponentInChildren<PolygonCollider2D>();
    }

    public Vector2 GetSize()
    {
        if (boxCollider != null) return Util.GetSizeBoxCollider2D(boxCollider);
        else if (polygonCollider != null) return Util.GetSizePolygonCollider2D(polygonCollider);
        else return Vector2.zero;
    }

    public Vector3 GetMiddlePos()
    {
        if (boxCollider != null) return Util.GetMiddlePosBoxCollider2D(boxCollider);
        else if (polygonCollider != null) return Util.GetMiddlePosPolygonCollider2D(polygonCollider);
        else return Vector3.zero;
    }

    public Vector3 GetBottomPos()
    {
        if (boxCollider != null) return Util.GetBottomPosBoxCollider2D(boxCollider);
        else if (polygonCollider != null) return Util.GetBottomPosPolygonCollider2D(polygonCollider);
        else return Vector3.zero;
    }
}