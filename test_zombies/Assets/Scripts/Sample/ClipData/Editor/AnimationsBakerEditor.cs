using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ClipDataAsset))]
public class ClipBakerEditor : Editor
{
  public override void OnInspectorGUI()
  {
    ClipDataAsset baker = (ClipDataAsset)target;
    if (GUILayout.Button("Bake Clip Asset"))
    {
      baker.Bake();
    }

    DrawDefaultInspector();
  }
}
