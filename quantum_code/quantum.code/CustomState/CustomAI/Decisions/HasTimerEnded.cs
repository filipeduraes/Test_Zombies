using System;
using Photon.Deterministic;

namespace Quantum
{
    [Serializable]
    public unsafe partial class HasTimerEnded : HFSMDecision
    {
        public AIBlackboardValueKey CurrentTimeKey;
        public override bool Decide(Frame f, EntityRef e, ref AIContext aiContext)
        {
            var bbComponent = f.Unsafe.GetPointer<AIBlackboardComponent>(e);
            var currentTime = bbComponent->GetFP(f, CurrentTimeKey.Key);
            
            return currentTime <= FP._0;
        }
    }
}