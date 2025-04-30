using UnityEditor;
using UnityEngine;

namespace Base.Editor
{
    public class Tools
    {
        [MenuItem("Tools/Clear Prefs")]
        public static void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
            Debug.LogWarning("All prefs deleted");
        }
    }
}
