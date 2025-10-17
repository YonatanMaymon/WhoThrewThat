using UnityEngine;

public static class Util
{
    private static float Z_VAL = -0.9f;

    /// <summary>
    /// Converts screen space coordinates to world space coordinates using a plane
    /// defined at a specific Z-value in world space.
    /// </summary>
    /// <param name="x">The x-coordinate in screen space.</param>
    /// <param name="y">The y-coordinate in screen space.</param>
    /// <returns>
    /// A <see cref="Vector3"/> representing the world space position corresponding
    /// to the given screen space coordinates. Returns <see cref="Vector3.zero"/> if
    /// the ray does not intersect the plane.
    /// </returns>
    public static Vector3 GetWorldSpacePos(float x, float y)
    {
        // Use the camera's forward direction to calculate the plane distance
        Plane plane = new Plane(Vector3.forward, new Vector3(0, 0, Z_VAL));
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y, 0));

        if (plane.Raycast(ray, out float distance))
            return ray.GetPoint(distance);

        // Fallback in case the ray doesn't hit the plane
        return Vector3.zero;
    }

    /// <summary>
    /// generates random spawn point above screen
    /// </summary>
    /// <param name="hightOffset">the offset above the screen</param>
    /// <returns>random spawn point above screen</returns>
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