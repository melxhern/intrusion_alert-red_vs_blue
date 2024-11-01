using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReportButtonUI : MonoBehaviour
{
    [SerializeField]
    private Button reportButton;

    public void SetInteractable(bool isInteractable)
    {
        reportButton.interactable = isInteractable;
    }

    public void OnClickButton()
    {
        var character = AmongUsRoomPlayer.MyRoomPlayer.myCharacter as IngameCharacterMover;
        character.Report();
    }
}
