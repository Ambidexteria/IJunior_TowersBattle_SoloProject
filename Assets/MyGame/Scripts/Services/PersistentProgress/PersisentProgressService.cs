using Base.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Services.PersistentProgress
{
    public class PersisentProgressService : IPersisentProgressService
    {
        public PlayerProgress PlayerProgress { get; set; }
    }
}
