using System;
using System.Collections;
using UnityEngine;
using UnityEditor;

namespace AnimationScript
{
    public class SmithyAnimationController : Singleton<SmithyAnimationController>
    {
        //[SerializeField] public int currentCps;
        //[SerializeField] public int oldCps;
        //[SerializeField] public int cpsRate;
        //[SerializeField] public int deltaCps;
        
        [SerializeField] public GameObject smithy;
        private Animator smithyAnimator;
        //private bool phase1, phase2, phase3;
        //private int currentCpsRate;

        private void Start()
        {
            /*oldCps = 0;
            currentCps = 0;
            deltaCps = 0;
            phase1 = true;*/
            smithyAnimator = smithy.GetComponent<Animator>();
            //StartCoroutine(Initialize());
        }

        private void CheckPhase()
        {
            if (currentCps <= 2)
            {
                phase1 = true;
                phase2 = false;
                phase3 = false;
            }
            else if (currentCps >= 3 && currentCps <= 6)
            {
                phase1 = false;
                phase2 = true;
                phase3 = false;
            }
            else if (currentCps > 6)
            {
                phase1 = false;
                phase2 = false;
                phase3 = true;
            }
        }

        private IEnumerator Initialize()
        {
            string resetString = "Reset";
            string phase1String = "Phase1";
            string phase2String = "Phase2";
            string phase3String = "Phase3";
            
            while (true)
            {
                CheckPhase();
                
                if (oldCps != currentCps)
                {
                    if (oldCps > currentCps)
                    {
                        deltaCps = oldCps - currentCps;
                    }
                    else if (currentCps > oldCps)
                    {
                        deltaCps = currentCps - oldCps;
                    }
                }
                
                if (deltaCps == 0)
                {
                    smithyAnimator.SetTrigger(resetString);
                }
                else if (deltaCps != 0)
                {
                    if (phase1)
                    {
                        smithyAnimator.SetBool(phase1String, true);
                        smithyAnimator.SetBool(phase2String, false);
                        smithyAnimator.SetBool(phase3String, false);
                        yield return new WaitForSeconds(1f);
                    }
                    else if (phase2)
                    {
                        smithyAnimator.SetBool(phase1String, false);
                        smithyAnimator.SetBool(phase2String, true);
                        smithyAnimator.SetBool(phase3String, false);
                        yield return new WaitForSeconds(1f);
                    }
                    else if (phase3)
                    {
                        smithyAnimator.SetBool(phase1String, false);
                        smithyAnimator.SetBool(phase2String, false);
                        smithyAnimator.SetBool(phase3String, true);
                        yield return new WaitForSeconds(1f);
                    }
                }
                
                oldCps = currentCps;
                currentCps = 0;
            }
        }

        public void Tired()
        {
            string tiredString = "Tired";
            smithyAnimator.SetTrigger(tiredString);
        }

        public void Hit()
        {
            string hitString = "Hit";
            if (phase1)
            {
                smithyAnimator.SetTrigger(hitString);
            }
            currentCps++;
        }
    }
    
    #if UNITY_EDITOR
    [CustomEditor(typeof(SmithyAnimationController))]
    public class SmithyAnimation : Editor
    {
        public override void OnInspectorGUI()
        {
            GUILayout.Label("Smithy Hit Animation Controller", EditorStyles.boldLabel);
            
            DrawDefaultInspector();
            
            var smithyController = (SmithyAnimationController) target;
            
            if (GUILayout.Button("Hit"))
            {
                smithyController.Hit();
            }
            else if (GUILayout.Button("Tired"))
            {
                smithyController.Tired();
            }
        }
    }
    #endif
}