using UnityEngine;

public static class Util
{
    private const float Z_VAL = -0.9f;

    public static Vector3 GetWorldSpacePos(float x, float y)
    {
        // Use the camera's forward direction to calculate the plane distance
        Plane plane = new(Vector3.forward, new Vector3(0, 0, Z_VAL));
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y, 0));

        if (plane.Raycast(ray, out float distance))
            return ray.GetPoint(distance);

        // Fallback in case the ray doesn't hit the plane
        return Vector3.zero;
    }

    public static Vector3 GenerateRandomSpawnPointAboveScreen(float hightOffset)
    {
        float widthOffset = GameManager.ScreenBufferX;
        float xSpawnScreenVal = Random.Range(widthOffset, Screen.width * (1 - widthOffset));
        Vector3 spawnPos = Util.GetWorldSpacePos(xSpawnScreenVal, Screen.height * hightOffset);
        return spawnPos;
    }

    public static Vector3 GenerateRandomVector3(float axisVariance)
    {
        return new Vector3(RandomFloat(axisVariance), RandomFloat(axisVariance), RandomFloat(axisVariance));
    }
    private static float RandomFloat(float range)
    {
        return Random.Range(-range, range);
    }
}