using System.Collections.Generic;
using Zenject;

namespace Base
{
    internal class BootstrapState : IState
    {
        public void Enter()
        {
            InstallServices();
        }

        public void Exit()
        {
        }

        private void InstallServices()
        {
            //new InputInstaller().InstallBindings();
        }
    }
}