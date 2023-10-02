using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorUtils
{
    /**
     * ÏòÁ¿µÄÂü¹þ¶Ù¾àÀë
     * d = |x1-x2|+|y1-y2|
     * d = |x1-x2|+|y1-y2|+|z1-z2|
     */
    public static float Distance(Vector2 a, Vector2 b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    public static float Distance(Vector3 a, Vector3 b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) + Mathf.Abs(a.z - b.z);
    }
}
