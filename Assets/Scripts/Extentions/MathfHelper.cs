using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    public static T GetRandom<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    public static float Normalize(this float value)
    {
        if (value > 0) return 1;
        if (value < 0) return -1;
        return 0;
    }

    public static List<T> GetRandom<T>(this List<T> list, int count)
    {
        List<T> result = new List<T>();
        var temp = list;
        for (int i = 0; i < count; i++)
        {
            result.Add(temp.GetRandom());
            temp = list.Except(result).ToList();
        }
        return result;
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
