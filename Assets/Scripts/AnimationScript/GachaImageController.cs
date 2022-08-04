using GameDatabase;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace AnimationScript
{
    //Just animation controller script for Gacha Images
    
    public class GachaImageController : MonoBehaviour
    {
        [SerializeField] private int maxRoll = 20;
        private int rolledCount;
        private Animator imageRolling;
        [SerializeField] public Sprite gachaResultSprite;
        [SerializeField] public OreDatabase oreDatabase;

        void Start()
        {
            imageRolling = gameObject.GetComponent<Animator>();
            ReRollImage();
            imageRolling.speed = maxRoll;
        }
     
        public void Counting()
        {
            rolledCount++;
            ReRollImage();
            imageRolling.speed = maxRoll - rolledCount;
             
            if (rolledCount > maxRoll - 2)
            {
                string stopTrigger = "Stop";
                imageRolling.SetTrigger(stopTrigger);
                gameObject.GetComponent<Image>().sprite = gachaResultSprite;
            }
        }
     
        public void ReRollImage()
        {
            int randomNumber = Random.Range(0, oreDatabase.premiumOres.Length);
            
            gameObject.GetComponent<Image>().sprite = oreDatabase.premiumOres[randomNumber].oreSprite;
        }
     
        public void SetResultImage(Sprite sprite)
        {
            gachaResultSprite = sprite;
        }
     }
}
