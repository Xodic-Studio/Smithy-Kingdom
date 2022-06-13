using UnityEngine;

namespace Ore
{
    [CreateAssetMenu(fileName = "New Ore", menuName = "Game/Ore")]
    public class OreStats : ScriptableObject
    {
        [Header("Changeable Values")]
        public string oreName;
        public Sprite oreSprite;
        public string oreDescription;
        public int currentHardness;
    
        [Header("Default Values")]
        public int defaultHardness;
        public bool isPremium;

    }
}
