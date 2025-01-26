using System;
using UnityEngine;

public static class Util
{
    public static T ParseEnumFromString<T>(string value)
    {
        return (T)Enum.Parse(typeof(T), value);
    }

    public static bool SetActive(GameObject gameObject, bool isOn)
    {
        if (gameObject != null && gameObject.activeSelf != isOn)
        {
            gameObject.SetActive(isOn);
            return true;
        }
        return false;
    }

    public static Vector2 GetSizeBoxCollider2D(BoxCollider2D boxCollider2D)
    {
        return new Vector2(boxCollider2D.size.x, boxCollider2D.size.y); ;
    }

    public static Vector3 GetMiddlePosBoxCollider2D(BoxCollider2D boxCollider2D)
    {
        return new Vector3(boxCollider2D.transform.position.x + boxCollider2D.offset.x, boxCollider2D.transform.position.y + boxCollider2D.offset.y, 0);
    }

    public static Vector3 GetBottomPosBoxCollider2D(BoxCollider2D boxCollider2D)
    {
        return new Vector3(boxCollider2D.transform.position.x + boxCollider2D.offset.x, boxCollider2D.transform.position.y + boxCollider2D.offset.y - boxCollider2D.size.y / 2, 0);
    }

    public static Vector2 GetSizePolygonCollider2D(PolygonCollider2D polygonCollider2D)
    {
        if (polygonCollider2D == null || polygonCollider2D.points.Length == 0)
        {
            Debug.LogError("PolygonCollider2D is null or has no points!");
            return Vector2.zero;
        }

        // 초기값 설정
        float minX = float.MaxValue, maxX = float.MinValue;
        float minY = float.MaxValue, maxY = float.MinValue;

        // 각 점을 순회하며 최소값과 최대값 계산
        foreach (Vector2 point in polygonCollider2D.points)
        {
            // Local Space를 World Space로 변환
            Vector2 worldPoint = polygonCollider2D.transform.TransformPoint(point);

            // X 값 비교
            if (worldPoint.x < minX) minX = worldPoint.x;
            if (worldPoint.x > maxX) maxX = worldPoint.x;

            // Y 값 비교
            if (worldPoint.y < minY) minY = worldPoint.y;
            if (worldPoint.y > maxY) maxY = worldPoint.y;
        }

        // 가장 먼 X 거리와 Y 거리 계산
        float xDistance = maxX - minX;
        float yDistance = maxY - minY;

        // 결과 반환
        return new Vector2(xDistance, yDistance);
    }

    public static Vector3 GetMiddlePosPolygonCollider2D(PolygonCollider2D polygonCollider2D)
    {
        Vector2[] points = polygonCollider2D.points;

        if (points.Length == 0)
        {
            Debug.LogError("PolygonCollider2D is null or has no points!");
            return Vector3.zero;
        }

        Vector2 sum = Vector2.zero;

        foreach (Vector2 point in points)
        {
            sum += point;
        }

        return sum / points.Length + polygonCollider2D.offset + (Vector2)polygonCollider2D.transform.position;
    }

    public static Vector3 GetBottomPosPolygonCollider2D(PolygonCollider2D polygonCollider2D)
    {
        Vector3 middlePos = GetMiddlePosPolygonCollider2D(polygonCollider2D);
        Vector2 size = GetSizePolygonCollider2D(polygonCollider2D);

        return new Vector3(middlePos.x, middlePos.y - size.y / 2, 0f);
    }

    public static void FlipLocalScaleX(GameObject gameObject)
    {
        FlipLocalScaleX(gameObject.transform);
    }

    public static void FlipLocalScaleX(Transform transform)
    {
        Vector3 scale = transform.localScale;
        scale.x = -scale.x; // x 값의 부호 반전
        transform.localScale = scale;
    }

    public static T FindComponentInHierarchy<T>(GameObject root) where T : Component
    {
        // 현재 GameObject에서 컴포넌트 찾기
        T component = root.GetComponent<T>();
        if (component != null)
        {
            return component;
        }

        // 자식 오브젝트에서 재귀적으로 탐색
        foreach (Transform child in root.transform)
        {
            component = FindComponentInHierarchy<T>(child.gameObject);
            if (component != null)
            {
                return component;
            }
        }

        return null; // 컴포넌트를 찾지 못한 경우
    }

    public static Vector2 GetLocalSize(SpriteRenderer spriteRenderer)
    {
        if (spriteRenderer != null && spriteRenderer.sprite != null)
        {
            Vector2 pixelSize = spriteRenderer.sprite.rect.size; // 픽셀 크기
            float pixelsPerUnit = spriteRenderer.sprite.pixelsPerUnit; // PPU 값

            DebugConsole.Log($"size is {(pixelSize / pixelsPerUnit).y}");
            return pixelSize / pixelsPerUnit; // 로컬 크기 반환
        }

        Debug.LogError("SpriteRenderer or Sprite is null!");
        return Vector2.zero;
    }

    public static GameObject GetMonsterGameObject(Collider2D collision)
    {
        return collision.gameObject.transform.parent?.gameObject;
    }

    public static int Round(int p, int q)
    {
        return (p + q - 1) / q;
    }
}