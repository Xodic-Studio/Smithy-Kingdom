using UnityEngine;
using UnityEditor;

public class EquipmentDropTest : MonoBehaviour
{
    [SerializeField] private GameObject equipmentDropPrefab;
    [SerializeField] public Transform dropZone;
    
    public void Spawn()
    {
        Instantiate(equipmentDropPrefab, new Vector3(-0.2f,-3.27f,0), Quaternion.identity ,dropZone);
    }
}

[CustomEditor(typeof(EquipmentDropTest))]
public class DropTest : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        var equipment = (EquipmentDropTest) target;
        
        if (GUILayout.Button("Spawn"))
        {
            equipment.Spawn();
        }
    }
}