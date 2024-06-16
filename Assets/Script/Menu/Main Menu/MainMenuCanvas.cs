using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _optionsCanvas;
    [SerializeField] private GameObject _newGameCanvas;
    [SerializeField] private GameObject _loadGameCanvas;

    public void OptionsClick()
    {
        gameObject.SetActive(false);
        _optionsCanvas.SetActive(true);
    }

    public void NewGameClick()
    {
        gameObject.SetActive(false);
        _newGameCanvas.SetActive(true);
    }

    public void LoadGameClick()
    {
        gameObject.SetActive(false);
        _loadGameCanvas.SetActive(true);
    }

    public void ExitClick()
    {
        Application.Quit();
    }
}
