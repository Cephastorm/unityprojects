using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace csl
{
    public class stormutils : MonoBehaviour
    {

        public static float TruncateFloat(float value, int precision)
        {
            float step = (float)System.Math.Pow(10, precision);
            float tmp = (float)System.Math.Truncate(step * value);
            return tmp / step;
        }
        public static bool CompareVectors(Vector3 a, Vector3 b, int precision)
        {
            return TruncateFloat(a.x, precision) == TruncateFloat(b.x, precision) &&
                TruncateFloat(a.y, precision) == TruncateFloat(b.y, precision) &&
                TruncateFloat(a.z, precision) == TruncateFloat(b.z, precision);


        }

    }
}
