using UnityEngine;

namespace Base.Data
{
    public class StageIconsDatabase : MonoBehaviour
    {
        [SerializeField] private Sprite[] _icons;

        public Sprite GetStageIcon(string iconName)
        {
            Sprite icon = null;

            foreach (Sprite tempIcon in _icons)
                if (tempIcon.name == iconName)
                    icon = tempIcon;

            if (icon == null)
                throw new System.NullReferenceException(nameof(icon));

            return icon;
        }
    }
}
