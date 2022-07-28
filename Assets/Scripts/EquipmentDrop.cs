using UnityEngine;
using UnityEditor;

public class EquipmentDrop : MonoBehaviour
{
    [SerializeField] private GameObject equipmentDropPrefab;
    [SerializeField] public Transform dropZone;
    
    public void Spawn()
    {
        Instantiate(equipmentDropPrefab, new Vector3(-0.2f,-3.27f,0), Quaternion.identity ,dropZone);
    }
}

[CustomEditor(typeof(EquipmentDrop))]
public class NormalDrop : Editor
{
    public override void OnInspectorGUI()
    {
        GUILayout.Label("Normal Drop", EditorStyles.boldLabel);

        DrawDefaultInspector();

        var equipment = (EquipmentDrop) target;

        if (GUILayout.Button("Spawn"))
        {
            equipment.Spawn();
        }
    }
}