using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(World))]
public class HexGridInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if(GUILayout.Button("Regenerate"))
        {
            World world = target as World;
            world.GenerateHexMap();
        }
    }

}
