using UnityEngine;

public class Math
{
    public static float RadToDeg360(float n)
    {
        return (n >= 0 ? n : 2 * Mathf.PI + n) * Mathf.Rad2Deg;
    }
}
