using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReportUI : MonoBehaviour
{
    [SerializeField]
    private Image deadbodyImg;

    [SerializeField]
    private Material material;

    public void Open(EPlayerColor deadbodyColor)
    {
        AmongUsRoomPlayer.MyRoomPlayer.myCharacter.IsMovealbe = false;

        Material inst = Instantiate(material);
        deadbodyImg.material = inst;

        gameObject.SetActive(true);

        deadbodyImg.material.SetColor("_PlayerColor", PlayerColor.GetColor(deadbodyColor));
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
