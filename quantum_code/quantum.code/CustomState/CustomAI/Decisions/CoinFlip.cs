using System;
using Photon.Deterministic;

namespace Quantum
{
    [Serializable]
    public unsafe partial class CoinFlip : HFSMDecision
    {
        public FP ValueRange;
        public FP TrueProbability;
        public override bool Decide(Frame f, EntityRef e, ref AIContext aiContext)
        {
            var randomValue = f.RNG->Next(FP._0, ValueRange);
            return randomValue <= ValueRange * TrueProbability;
        }
    }
}