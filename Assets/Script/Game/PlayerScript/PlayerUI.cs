using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI promptMessage;

    private void Awake()
    {
        if (!promptMessage)
        {
            promptMessage = GameObject.FindGameObjectWithTag("InteractionUI").GetComponent<TextMeshProUGUI>();
        }
    }

    public void UpdateText(string text)
    {
        promptMessage.text = text;
    }
}
