using UnityEngine;

public static class MathfHelper
{
    public static float InverseLerp(Vector3 a, Vector3 b, Vector3 value)
    {
        Vector3 AB = b - a;
        Vector3 AV = value - a;
        return Vector3.Dot(AV, AB) / Vector3.Dot(AB, AB);
    }

    public static Vector3 PointOnLine(Vector3 a, Vector3 b, Vector3 p)
    {
        return a + Vector3.Project(p - a, b - a);
    }

    public static Vector3 RandomXZ(float range)
    {
        return new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));
    }

    public static bool RamdomBool(float chanse)
    {
        return Random.Range(0f, 1f) < chanse;
    }
}
