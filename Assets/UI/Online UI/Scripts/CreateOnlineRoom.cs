using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class CreateOnlineRoom : MonoBehaviour
{
    [SerializeField]
    private InputField nicknameInputField;

    // [SerializeField]
    // private GameObject createRoomUI;

    private CreateGameRoomData roomData;

    private void Start()
    {
        // Initialize room data with default values
        roomData = new CreateGameRoomData() { imposterCount = 1, maxPlayerCount = 4 };
    }

    public void OnClickCreateRoomButton()
    {
        if (nicknameInputField.text != "" && nicknameInputField.text != "Pseudo")
        {
            PlayerSettings.nickname = nicknameInputField.text;
            CreateRoom();
            gameObject.SetActive(false);
        }
        else
        {
            nicknameInputField.GetComponent<Animator>().SetTrigger("on");
        }
    }

    public void OnClickEnterGameRoomButton()
    {
        if (nicknameInputField.text != "" && nicknameInputField.text != "Pseudo")
        {
            PlayerSettings.nickname = nicknameInputField.text;
            var manager = AmongUsRoomManager.singleton;
            manager.StartClient();
        }
        else
        {
            nicknameInputField.GetComponent<Animator>().SetTrigger("on");
        }
    }

    public void CreateRoom()
    {
        var manager = NetworkManager.singleton as AmongUsRoomManager;
        manager.minPlayerCount = 3;
        manager.imposterCount = 1;
        manager.maxConnections = 4;
        manager.StartHost();
    }

    public class CreateGameRoomData
    {
        public int imposterCount;
        public int maxPlayerCount;
    }
}
