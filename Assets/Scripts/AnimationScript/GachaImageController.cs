using GameDatabase;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace AnimationScript
{
    //Just animation controller script for Gacha Images
    
    public class GachaImageController : Singleton<GachaImageController>
    {
        string _stopString = "Stop";
        [SerializeField] private int maxRoll = 20;
        private int rolledCount;
        private Animator imageRolling;
        [SerializeField] public Sprite gachaResultSprite;
        [SerializeField] public OreDatabase oreDatabase;
        private bool skipped, rolling;

        void Start()
        {
            rolling = true;
            imageRolling = gameObject.GetComponent<Animator>();
            ReRollImage();
            imageRolling.speed = maxRoll;
        }

        public void Skip()
        {
            if (rolling)
            {
                rolling = false;
                skipped = true;
                rolledCount = maxRoll;
                imageRolling.speed = 1;
                imageRolling.SetTrigger(_stopString);
                gameObject.GetComponent<Image>().sprite = gachaResultSprite;
            }
        }
        
        public void Counting()
        {
            if (rolledCount < maxRoll)
            {
                rolledCount++;
                ReRollImage();
                imageRolling.speed = maxRoll - rolledCount;
            }

            if (!skipped)
            {
                if (rolledCount > maxRoll - 2)
                {
                    rolling = false;
                    imageRolling.SetTrigger(_stopString);
                    gameObject.GetComponent<Image>().sprite = gachaResultSprite;
                }
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
