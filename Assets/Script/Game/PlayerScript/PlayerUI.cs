using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class PlayerUI : NetworkBehaviour
{
    [SerializeField]
    private TextMeshProUGUI promptMessage;

    public override void OnStartLocalPlayer()
    {
        if (!promptMessage)
        {
            promptMessage = GameObject.FindGameObjectWithTag("InteractionUI").GetComponent<TextMeshProUGUI>();
        }
    }

    public void UpdateText(string text)
    {
        if (!isLocalPlayer) return;

        promptMessage.text = text;
    }
}
