using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuCanvas;

    [SerializeField] private Slider _volumeSlider;

    private void Awake()
    {
        OnVolumeSliderChanged();
    }

    public void OnVolumeSliderChanged()
    {
        AudioListener.volume = _volumeSlider.value;
    }

    public void BackClick()
    {
        gameObject.SetActive(false);
        _mainMenuCanvas.SetActive(true);
    }
}
