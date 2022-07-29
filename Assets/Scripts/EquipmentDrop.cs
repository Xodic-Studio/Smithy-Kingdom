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
            GameObject equipmentFirstDrop = Instantiate(equipmentDropPrefab, new Vector3(-0.2f,-3.27f,0), Quaternion.identity ,dropZone);
            equipmentFirstDrop.GetComponent<Animator>().SetTrigger(firstDrop);
        }
    }
}

#if UNITY_EDITOR
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
#endif