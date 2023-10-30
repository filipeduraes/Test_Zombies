using Photon.Deterministic;

namespace Quantum 
{
  partial class RuntimePlayer
  {
      public AssetRefEntityPrototype EntityPrototypeAsset;
      
      partial void SerializeUserData(BitStream stream)
      {
          stream.Serialize(ref EntityPrototypeAsset.Id.Value);
      }
  }
}
