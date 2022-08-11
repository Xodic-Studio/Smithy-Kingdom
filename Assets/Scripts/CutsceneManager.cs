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
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Scenes/Main Scene");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
