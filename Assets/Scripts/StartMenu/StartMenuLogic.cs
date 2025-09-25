using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuLogic : MonoBehaviour
{
    [SerializeField] private StartMenuUILogic _uiLogic;
    [SerializeField] private StartMenuAudioManagerLogic _audioManager;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void QuitGame()
    {
        _audioManager.PlayClickSound();
        Application.Quit();
    }


    public void ShowControls()
    {
        _audioManager.PlayClickSound();
        _uiLogic.ShowControls();
    }

    public void HideControls()
    {
        _audioManager.PlayClickSound();
        _uiLogic.HideControls();
    }

    public void PlayGame()
    {
        _audioManager.PlayClickSound();
        LoadingSceneLogic.nextScene = "GameScene";
        SceneManager.LoadScene("LoadingScreen");
    }
}