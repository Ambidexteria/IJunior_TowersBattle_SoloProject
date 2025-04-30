using UnityEngine;
using Zenject;

namespace Base
{
    public class ZenjectProjectContextBindingsWatcher : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        [ContextMenu(nameof(ShowAllContracts))]
        public void ShowAllContracts()
        {
            string text = "Zenject: All contracts\n\n";

            ProjectContext context = ProjectContext.Instance;

            var contracts = context.Container.AllContracts;

            foreach (var contract in contracts)
                if (contract.Type.Namespace != nameof(Zenject))
                    text += $"{nameof(contract)} type - {contract.Type}\n";

            Debug.Log(text);
        }

        [ContextMenu(nameof(ShowBindedInterfaces))]
        public void ShowBindedInterfaces()
        {
            string text = "Zenject: binded interfaces\n\n";

            ProjectContext context = ProjectContext.Instance;

            var contracts = context.Container.AllContracts;

            foreach (var contract in contracts)
                if (contract.Type.Namespace != nameof(Zenject))
                    if (contract.Type.IsInterface)
                        text += $"{nameof(contract)} type - {contract.Type}\n";

            Debug.Log(text);
        }

        [ContextMenu(nameof(ShowBindedClasses))]
        public void ShowBindedClasses()
        {
            string text = "Zenject: binded classes\n\n";

            ProjectContext context = ProjectContext.Instance;

            var contracts = context.Container.AllContracts;

            foreach (var contract in contracts)
                if (contract.Type.Namespace != nameof(Zenject))
                    if (contract.Type.IsClass)
                        text += $"{nameof(contract)} type - {contract.Type}\n";

            Debug.Log(text);
        }
    }
}
