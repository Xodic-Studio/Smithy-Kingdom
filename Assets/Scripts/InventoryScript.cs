using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public OreDatabase oreDatabase;
    public OreStats[] ownedOres;
    public OreStats[] OwnedOres => ownedOres;

    public void Start()
    {
        ownedOres = oreDatabase.ores;
    }


}
