using UnityEngine;

namespace UnityExtensions
{
    public static class UnityVector3Extensions
    {
        public static Vector3 AddY(this Vector3 vector, float y)
        {
            vector.y += y;
            return vector;
        }

        public static Vector3 AddX(this Vector3 vector, float x)
        {
            vector.y += x;
            return vector;
        }

        public static Vector3 AddZ(this Vector3 vector, float z)
        {
            vector.y += z;
            return vector;
        }
    }
}
