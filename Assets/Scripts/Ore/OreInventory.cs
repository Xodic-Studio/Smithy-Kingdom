using UnityEngine;

namespace Ore
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Game/Ore Inventory")]
    public class OreInventory : ScriptableObject
    {
        public OreStats[] ores;
        public OreStats[] allOres;

        public OreStats[] AllOres => allOres;
    
    }
}
