// <auto-generated>
// This code was auto-generated by a tool, every time
// the tool executes this code will be reset.
//
// If you need to extend the classes generated to add
// fields or methods to them, please create partial  
// declarations in another file.
// </auto-generated>

using Quantum;
using UnityEngine;

[CreateAssetMenu(menuName = "Quantum/AIFunction/AIFunction<T>/DefaultAIFunctionFPVector2", order = Quantum.EditorDefines.AssetMenuPriorityStart + 3)]
public partial class DefaultAIFunctionFPVector2Asset : AIFunctionAsset<Photon.Deterministic.FPVector2> {
  public Quantum.DefaultAIFunctionFPVector2 Settings_DefaultAIFunctionFPVector2;

  public override string AssetObjectPropertyPath => nameof(Settings_DefaultAIFunctionFPVector2);
  
  public override Quantum.AssetObject AssetObject => Settings_DefaultAIFunctionFPVector2;
  
  public override void Reset() {
    if (Settings_DefaultAIFunctionFPVector2 == null) {
      Settings_DefaultAIFunctionFPVector2 = new Quantum.DefaultAIFunctionFPVector2();
    }
    base.Reset();
  }
}

public static partial class DefaultAIFunctionFPVector2AssetExts {
  public static DefaultAIFunctionFPVector2Asset GetUnityAsset(this DefaultAIFunctionFPVector2 data) {
    return data == null ? null : UnityDB.FindAsset<DefaultAIFunctionFPVector2Asset>(data);
  }
}
