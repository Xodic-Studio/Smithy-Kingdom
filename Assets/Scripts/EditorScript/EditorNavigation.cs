#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EditorScript
{
    public class EditorNavigation : MonoBehaviour
    {
        private UIManager _uiManager;
        public UIManager ui => _uiManager;

        private void OnValidate()
        {
            _uiManager = GetComponent<UIManager>();
        }
    
    }
    
    [CustomEditor(typeof(EditorNavigation))]
    public class Navigation : Editor
    {
        public override void OnInspectorGUI()
        {
            var nav = (EditorNavigation) target;
            GUILayout.Label("Navigation", EditorStyles.largeLabel);
            UINavigation();
        
        
            void UINavigation()
            {
                EditorGUILayout.Space();
                if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(20)))
                {
                    nav.ui.CloseMenu();
                    EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
                }
                EditorGUILayout.Space();
            
                EditorGUILayout.Space();
                GUILayout.Label("Upgrades", EditorStyles.boldLabel);
                GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
                if (GUILayout.Button("UpgradeMenu",GUILayout.Width(150))) nav.ui.OpenUpgradeMenu();
                if (GUILayout.Button("PremiumUpgradeMenu",GUILayout.Width(150))) nav.ui.OpenPremiumUpgradeMenu();
                GUILayout.EndHorizontal();
            
                EditorGUILayout.Space();
                GUILayout.Label("Collections", EditorStyles.boldLabel);
                GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
                if (GUILayout.Button("CollectionMenu",GUILayout.Width(150))) nav.ui.OpenCollectionMenu();
                if (GUILayout.Button("AchievementMenu",GUILayout.Width(150))) nav.ui.OpenAchievementMenu();
                GUILayout.EndHorizontal();
            
                EditorGUILayout.Space();
                GUILayout.Label("Others", EditorStyles.boldLabel);
                GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
                if (GUILayout.Button("PremiumMenu")) nav.ui.OpenPremiumMenu();
                if (GUILayout.Button("SettingsMenu")) nav.ui.OpenSettingsMenu();
                if (GUILayout.Button("OreMenu")) nav.ui.OpenOreMenu();
                if (GUILayout.Button("PrestigeMenu")) nav.ui.OpenPrestigeMenu();
                GUILayout.EndHorizontal();
            }
        }
    }
}
#endif