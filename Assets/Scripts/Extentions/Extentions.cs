using UnityEngine;

public static class Extentions
{
    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public static bool HasComponent<T>(this GameObject gameObject)
    {
        return gameObject.GetComponent<T>() != null;
    }

    public static bool HasComponent<T>(this Collision collision)
    {
        return collision.gameObject.GetComponent<T>() != null;
    }

    public static bool HasComponent<T>(this Collider other)
    {
        return other.gameObject.GetComponent<T>() != null;
    }
}
