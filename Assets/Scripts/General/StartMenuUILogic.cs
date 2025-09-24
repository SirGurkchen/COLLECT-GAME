using UnityEngine;

public class StartMenuUILogic : MonoBehaviour
{
    [SerializeField] private GameObject _menuButtons;
    [SerializeField] private GameObject _controlsUI;

    public void ShowControls()
    {
        _menuButtons.SetActive(false);
        _controlsUI.SetActive(true);
    }

    public void HideControls()
    {
        _controlsUI.SetActive(false);
        _menuButtons.SetActive(true);
    }
}