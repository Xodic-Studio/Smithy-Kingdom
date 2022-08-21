using System.Collections;
using UnityEngine;
using UnityEditor;

namespace AnimationScript
{
    public class SmithyAnimationController : Singleton<SmithyAnimationController>
    {
        private string _hitString = "Hit";
        private string _tiredString = "Tired";
        private string _resetString = "Reset";
        private string _phaseString = "Phase";
        public int phase;
        public bool timerActive;
        [SerializeField] public float hit1, hit2, hit3;
        [SerializeField] public int hit;
        [SerializeField] public bool isTired;
        
        [SerializeField] public GameObject smithy;
        [SerializeField] private Animator _smithyAnimator;
        
        
        private void Start()
        {
            SetPhase(0);
            hit = 0;
            timerActive = false;
            _smithyAnimator = smithy.GetComponent<Animator>();
        }

        private void Update()
        {
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
                StopAllCoroutines();
                //StopCoroutine("CountdownTimer");
                StartCoroutine(tempTimer);
            }
        }

        private IEnumerator CountdownTimer(float timer)
        {
            yield return new WaitForSeconds(timer);
            Reset();
        }

        public IEnumerator Tired()
        {
            isTired = true;
            _smithyAnimator.SetTrigger(_tiredString);
            yield return new WaitForSeconds(1f);
            isTired = false;
        }

        public void Hit()
        {
            if (isTired)
            {
                //Do nothing
            }
            else
            {
                hit++;
                switch (phase)
                {
                    case (0):
                        SetPhase(1);
                        StartTimer(hit1);
                        _smithyAnimator.SetTrigger(_hitString);
                        break;
                    case (1):
                        if (hit >= 2)
                        {
                            SetPhase(2);
                            StartTimer(hit2);
                        }
                        else
                        {
                            SetPhase(1);
                            StartTimer(hit1);
                        }
                        _smithyAnimator.SetTrigger(_hitString);
                        break;
                    case (2):
                        if (hit >= 4)
                        {
                            SetPhase(3);
                            StartTimer(hit3);
                        }
                        else
                        {
                            SetPhase(2);
                            StartTimer(hit2);
                        }
                        _smithyAnimator.SetTrigger(_hitString);
                        break;
                    case (3):
                        StartTimer(hit3);
                        _smithyAnimator.SetTrigger(_hitString);
                        break;
                }
            }
        }

        private void SetPhase(int number)
        {
            phase = number;
            _smithyAnimator.SetInteger(_phaseString, number);
        }

        public void Reset()
        {
            timerActive = false;
            if (phase != 1)
            {
                _smithyAnimator.SetTrigger(_resetString);
            }
            hit = 0;
            SetPhase(0);
        }
        
        // For testing button x4 for timing press
        public IEnumerator Wait(float time)
        {
            for (int i = 0; i < 5; i++)
            {
                Hit();
                yield return new WaitForSeconds(time);
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
            else if (GUILayout.Button("Hit x4"))
            {
                smithyController.Hit();
                smithyController.Hit();
                smithyController.Hit();
                smithyController.Hit();
                //smithyController.StartCoroutine(smithyController.Wait(0.02f));
            }
            else if (GUILayout.Button("Tired"))
            {
                smithyController.StartCoroutine(smithyController.Tired());
            }
            else if (GUILayout.Button("Reset Animation (Has Exit Time)"))
            {
                smithyController.Reset();
            }
        }
    }
    #endif
}