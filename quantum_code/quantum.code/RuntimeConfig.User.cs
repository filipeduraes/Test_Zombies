using Photon.Deterministic;

namespace Quantum 
{
    partial class RuntimeConfig
    {
        public AssetRefAIBlackboardInitializer BlackboardInitializer;
        public int GridSize = 10;

        partial void SerializeUserData(BitStream stream)
        {
            stream.Serialize(ref BlackboardInitializer.Id.Value);
        }
    }
}