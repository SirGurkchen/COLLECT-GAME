using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneLogic : MonoBehaviour
{
    [SerializeField] private const float LOAD_TIMER = 1f;

    public static string nextScene;

    private void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        yield return new WaitForSeconds(LOAD_TIMER);

        SceneManager.LoadScene(nextScene);
    }
}