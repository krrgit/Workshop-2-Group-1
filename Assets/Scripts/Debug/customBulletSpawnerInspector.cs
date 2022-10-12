using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BulletHellSpawner))]
public class customBulletSpawnerInspector : Editor
{
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        BulletHellSpawner spawner = (BulletHellSpawner)target;
        if (GUILayout.Button("Update Spawner")) {
            spawner.UpdateColumns();
        }
        
    }
}
