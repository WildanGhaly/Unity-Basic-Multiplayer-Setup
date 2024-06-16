using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadGameCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuCanvas;
    [SerializeField] private GameObject _slotList;

    private int _slotIDClicked = 0;
    private Color _selectedColor = Color.blue;
    private Color _defaultColor = Color.white;

    public void SlotClick(int id)
    {
        _slotIDClicked = id;

        // Loop through all slots
        for (int i = 0; i < _slotList.transform.childCount; i++)
        {
            Button button = _slotList.transform.GetChild(i).GetChild(0).GetComponent<Button>();
            if (i != _slotIDClicked)
            {
                // Set the default color for non-selected buttons
                ColorBlock cb = button.colors;
                cb.normalColor = _defaultColor;
                cb.selectedColor = _defaultColor;
                button.colors = cb;
            }
            else
            {
                // Set the selected color for the clicked button
                ColorBlock cb = button.colors;
                cb.normalColor = _selectedColor;
                cb.selectedColor = _selectedColor;
                button.colors = cb;
            }
        }
    }

    public void DeleteClick()
    {
        // TODO: Delete clicked slot
    }

    public void LoadClick()
    {
        // TODO: Load the game based on selected slot
    }

    public void BackClick()
    {
        gameObject.SetActive(false);
        _mainMenuCanvas.SetActive(true);
    }
}
