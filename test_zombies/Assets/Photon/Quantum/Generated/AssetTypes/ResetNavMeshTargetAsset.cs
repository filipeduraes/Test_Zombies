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

[CreateAssetMenu(menuName = "Quantum/AIAction/ResetNavMeshTarget", order = Quantum.EditorDefines.AssetMenuPriorityStart + 17)]
public partial class ResetNavMeshTargetAsset : AIActionAsset {
  public Quantum.ResetNavMeshTarget Settings;

  public override Quantum.AssetObject AssetObject => Settings;
  
  public override void Reset() {
    if (Settings == null) {
      Settings = new Quantum.ResetNavMeshTarget();
    }
    base.Reset();
  }
}

public static partial class ResetNavMeshTargetAssetExts {
  public static ResetNavMeshTargetAsset GetUnityAsset(this ResetNavMeshTarget data) {
    return data == null ? null : UnityDB.FindAsset<ResetNavMeshTargetAsset>(data);
  }
}