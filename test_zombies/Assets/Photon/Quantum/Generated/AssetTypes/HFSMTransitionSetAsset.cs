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

[CreateAssetMenu(menuName = "Quantum/HFSMTransitionSet", order = Quantum.EditorDefines.AssetMenuPriorityStart + 182)]
public partial class HFSMTransitionSetAsset : AssetBase {
  public Quantum.HFSMTransitionSet Settings;

  public override Quantum.AssetObject AssetObject => Settings;
  
  public override void Reset() {
    if (Settings == null) {
      Settings = new Quantum.HFSMTransitionSet();
    }
    base.Reset();
  }
}

public static partial class HFSMTransitionSetAssetExts {
  public static HFSMTransitionSetAsset GetUnityAsset(this HFSMTransitionSet data) {
    return data == null ? null : UnityDB.FindAsset<HFSMTransitionSetAsset>(data);
  }
}