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

[CreateAssetMenu(menuName = "Quantum/AIFunction/AIFunction<T>/DefaultAIFunctionAssetRef", order = Quantum.EditorDefines.AssetMenuPriorityStart + 3)]
public partial class DefaultAIFunctionAssetRefAsset : AIFunctionAsset<Quantum.AssetRef> {
  public Quantum.DefaultAIFunctionAssetRef Settings_DefaultAIFunctionAssetRef;

  public override string AssetObjectPropertyPath => nameof(Settings_DefaultAIFunctionAssetRef);
  
  public override Quantum.AssetObject AssetObject => Settings_DefaultAIFunctionAssetRef;
  
  public override void Reset() {
    if (Settings_DefaultAIFunctionAssetRef == null) {
      Settings_DefaultAIFunctionAssetRef = new Quantum.DefaultAIFunctionAssetRef();
    }
    base.Reset();
  }
}

public static partial class DefaultAIFunctionAssetRefAssetExts {
  public static DefaultAIFunctionAssetRefAsset GetUnityAsset(this DefaultAIFunctionAssetRef data) {
    return data == null ? null : UnityDB.FindAsset<DefaultAIFunctionAssetRefAsset>(data);
  }
}
