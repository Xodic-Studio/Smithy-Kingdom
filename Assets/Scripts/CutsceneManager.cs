using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadMainGame());
    }
    IEnumerator LoadMainGame()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("Scenes/Main Scene");
    }
}
