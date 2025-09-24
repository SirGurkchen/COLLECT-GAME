using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuLogic : MonoBehaviour
{
    [SerializeField] private StartMenuUILogic _uiLogic;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;

    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void ShowControls()
    {
        _uiLogic.ShowControls();
    }

    public void HideControls()
    {
        _uiLogic.HideControls();
    }

    public void PlayGame()
    {
        LoadingSceneLogic.nextScene = "GameScene";
        SceneManager.LoadScene("LoadingScreen");
    }
}