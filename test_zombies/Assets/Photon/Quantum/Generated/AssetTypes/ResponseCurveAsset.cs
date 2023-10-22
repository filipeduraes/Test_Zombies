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

[CreateAssetMenu(menuName = "Quantum/AIFunction/AIFunction<T>/ResponseCurve", order = Quantum.EditorDefines.AssetMenuPriorityStart + 17)]
public partial class ResponseCurveAsset : AIFunctionAsset<Photon.Deterministic.FP> {
  public Quantum.ResponseCurve Settings_ResponseCurve;

  public override string AssetObjectPropertyPath => nameof(Settings_ResponseCurve);
  
  public override Quantum.AssetObject AssetObject => Settings_ResponseCurve;
  
  public override void Reset() {
    if (Settings_ResponseCurve == null) {
      Settings_ResponseCurve = new Quantum.ResponseCurve();
    }
    base.Reset();
  }
}

public static partial class ResponseCurveAssetExts {
  public static ResponseCurveAsset GetUnityAsset(this ResponseCurve data) {
    return data == null ? null : UnityDB.FindAsset<ResponseCurveAsset>(data);
  }
}