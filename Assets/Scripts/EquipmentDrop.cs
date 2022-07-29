using UnityEngine;
using UnityEditor;

public class EquipmentDrop : MonoBehaviour
{
    [SerializeField] public GameObject equipmentDropPrefab;
    [SerializeField] public Transform dropZone;
    
    public void Spawn(string dropType)
    {
        if (dropType == "Normal")
        {
            Instantiate(equipmentDropPrefab, new Vector3(-0.2f,-3.27f,0), Quaternion.identity ,dropZone);
        }
        else if (dropType == "First")
        {
            string firstDrop = "First Drop";
            GameObject drop = Instantiate(equipmentDropPrefab, new Vector3(-0.2f,-3.27f,0), Quaternion.identity ,dropZone);
            drop.GetComponent<Animator>().SetBool(firstDrop, true);
        }
    }
}

[CustomEditor(typeof(EquipmentDrop))]
public class NormalDrop : Editor
{
    public override void OnInspectorGUI()
    {
        GUILayout.Label("Spawn Drop", EditorStyles.boldLabel);

        DrawDefaultInspector();

        var equipment = (EquipmentDrop) target;

        if (GUILayout.Button("Spawn (Normal Drop)"))
        {
            equipment.Spawn("Normal");
        }
        else if (GUILayout.Button("Spawn (First Drop)"))
        {
            equipment.Spawn("First");
        }
    }
}