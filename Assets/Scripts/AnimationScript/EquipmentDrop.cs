using UnityEngine;
using UnityEditor;

namespace AnimationScript
{
    /*
    Noted:
    To use animation please use function "GetEquipmentResult(bool firstDrop, Sprite Sprite)".
    
    Examples:
    "GetEquipmentResult(false, sprite)" << in this case normal drop animation.
    "GetEquipmentResult(true, sprite)" << in this case first drop animation.
    */
    
    public class EquipmentDrop : Singleton<EquipmentDrop>
    {
        [SerializeField] public GameObject equipmentDropPrefab;
        [SerializeField] public Transform dropZone;
        public Sprite dummySprite;
    
        public void GetEquipmentResult(bool firstDrop, Sprite sprite)
        {
            if (!firstDrop)
            {
                GameObject equipmentNormalDrop = Instantiate(equipmentDropPrefab, new Vector3(-0.2f, -3.27f, 0), Quaternion.identity, dropZone);
                equipmentNormalDrop.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
            }
            else
            {
                string firstDropTrigger = "First Drop";
                GameObject equipmentFirstDrop = Instantiate(equipmentDropPrefab, new Vector3(-0.2f,-3.27f,0), Quaternion.identity ,dropZone);
                equipmentFirstDrop.GetComponent<Animator>().SetTrigger(firstDropTrigger);
                equipmentFirstDrop.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
            }
        }
    }

    [CustomEditor(typeof(EquipmentDrop))]
    public class SpawnEquipmentDrop : Editor
    {
        public override void OnInspectorGUI()
        {
            GUILayout.Label("Spawn Drop", EditorStyles.boldLabel);

            DrawDefaultInspector();

            var equipment = (EquipmentDrop) target;

            if (GUILayout.Button("Spawn (Normal Drop)"))
            {
                equipment.GetEquipmentResult(false, equipment.dummySprite);
            }
            else if (GUILayout.Button("Spawn (First Drop)"))
            {
                equipment.GetEquipmentResult(true, equipment.dummySprite);
            }
        }
    }
}