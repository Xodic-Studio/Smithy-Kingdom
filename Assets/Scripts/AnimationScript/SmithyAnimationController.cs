using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEditor;

namespace AnimationScript
{
    public class SmithyAnimationController : Singleton<SmithyAnimationController>
    {
        private string _hitString = "Hit";
        private string _tiredString = "Tired";
        private string _resetString = "Reset";
        public int phase;
        public bool timerActive;
        [SerializeField] public float hit1, hit2, hit3;
        
        [SerializeField] public GameObject smithy;
        private Animator _smithyAnimator;

        private void Start()
        {
            phase = 0;
            timerActive = false;
            _smithyAnimator = smithy.GetComponent<Animator>();
        }

        private void StartTimer(float timer)
        {
            var tempTimer = CountdownTimer(timer);
            if (!timerActive)
            {
                timerActive = true;
                StartCoroutine(tempTimer);
            }
            else if (timerActive)
            {
                tempTimer.Reset();
                StartCoroutine(tempTimer);
            }
        }

        private IEnumerator CountdownTimer(float timer)
        {
            yield return new WaitForSeconds(timer);
            timerActive = false;
            _smithyAnimator.SetTrigger(_resetString);
            /*if ()
            {
                timerActive = false;
            }*/
        }

        public void Tired()
        {
            _smithyAnimator.SetTrigger(_tiredString);
        }

        public void Hit()
        {
            switch (phase)
            {
                case (0):
                    phase = 1;
                    _smithyAnimator.SetTrigger(_hitString);
                    StartTimer(hit1);
                    break;
                case (1):
                    _smithyAnimator.SetTrigger(_hitString);
                    StartTimer(hit1);
                    break;
                case (2):
                    StartTimer(hit2);
                    break;
                case (3):
                    StartTimer(hit3);
                    break;
            }
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
            else if (GUILayout.Button("Hit x2"))
            {
                smithyController.Hit();
                smithyController.Hit();
            }
            else if (GUILayout.Button("Hit x3"))
            {
                smithyController.Hit();
                smithyController.Hit();
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