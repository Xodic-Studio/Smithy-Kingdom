using UnityEngine;

public class SmithySkin : MonoBehaviour
{
    [HideInInspector] public GameObject smithySweat;
    [HideInInspector] public GameObject smithyDepression1;
    [HideInInspector] public GameObject smithyDepression2;
    [SerializeField] public bool isTired;

    private void Update()
    {
        if (isTired)
        {
            smithySweat.SetActive(true);
            smithyDepression1.SetActive(true);
            smithyDepression2.SetActive(true);
        }
        else if (!isTired)
        {
            smithySweat.SetActive(false);
            smithyDepression1.SetActive(false);
            smithyDepression2.SetActive(false);
        }
    }
}