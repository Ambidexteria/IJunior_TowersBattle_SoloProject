using UnityEngine;

namespace Base.Data
{
    public static class DataExtensions
    {
        public static Vector3Data AsVector3Data(this Vector3 vector)
        {
            return new Vector3Data(vector.x, vector.y, vector.z);
        }

        public static Vector3 AsUnityVector3(this Vector3Data vector3data)
        {
            return new Vector3(vector3data.X, vector3data.Y, vector3data.Z);
        }

        public static T ToDeserialized<T>(this string json)
        {
            return JsonUtility.FromJson<T>(json);
        }
    }
}
