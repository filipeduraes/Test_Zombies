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


public partial class HFSMAndDecisionAsset : HFSMLogicalDecisionAsset {
  public Quantum.HFSMAndDecision Settings;

  public override Quantum.AssetObject AssetObject => Settings;
  
}

public static partial class HFSMAndDecisionAssetExts {
  public static HFSMAndDecisionAsset GetUnityAsset(this HFSMAndDecision data) {
    return data == null ? null : UnityDB.FindAsset<HFSMAndDecisionAsset>(data);
  }
}
