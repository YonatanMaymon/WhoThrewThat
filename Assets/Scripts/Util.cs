using UnityEngine;

public static class Util
{
    private static float Z_VAL = 0f;

    public static Vector3 GetWorldSpacePos(float x, float y)
    {
        float zDistance = Mathf.Abs(Camera.main.transform.position.z - Z_VAL);
        return Camera.main.ScreenToWorldPoint(new Vector3(x, y, zDistance));
    }
    /// <summary>
    /// generates random spawn point above screen
    /// </summary>
    /// <param name="widthOffset">the offset from the sides of the screen</param>
    /// <param name="hightOffset">the offset above the screen</param>
    /// <returns>random spawn point above screen</returns>
    public static Vector3 GenerateRandomSpawnPointAboveScreen(float widthOffset, float hightOffset)
    {
        widthOffset = Mathf.Clamp(widthOffset, 0.5f, 1f);
        float xSpawnScreenVal = Random.Range(Screen.width * (1 - widthOffset), Screen.width * widthOffset);
        Vector3 spawnPos = Util.GetWorldSpacePos(xSpawnScreenVal, Screen.height * hightOffset);
        return spawnPos;
    }
}