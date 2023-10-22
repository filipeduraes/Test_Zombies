using System;

namespace Quantum
{
    [Serializable]
    public unsafe partial class CoinFlipFiftyFifty : HFSMDecision
    {
        public override bool Decide(Frame f, EntityRef e, ref AIContext aiContext)
        {
            bool flip = f.RNG->Next(0, 1) < 1;
            return flip;
        }
    }
}