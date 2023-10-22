using System;

namespace Quantum
{
    [Serializable]
    public unsafe partial class ResetNavMeshTarget : AIAction
    {
        public override void Update(Frame f, EntityRef e, ref AIContext aiContext)
        {
            var navMeshPathfinder = f.Unsafe.GetPointer<NavMeshPathfinder>(e);
            navMeshPathfinder->Stop(f, e, true);
        }
    }
}