using UnityEngine;

[CreateAssetMenu (fileName = "Database", menuName = "Database")]
public class Database : ScriptableObject
{
    public UpgradeDatabase UpgradeDatabase { get; private set; }
    public UpgradeDatabase PremiumUpgradeDatabase{ get; private set; }
    public ItemDatabase ItemsDatabase{ get; private set; }
    public OreDatabase OresDatabase{ get; private set; }
    

}
