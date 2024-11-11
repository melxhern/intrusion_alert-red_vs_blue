using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Rendering;

public class AmongUsRoomPlayer : NetworkRoomPlayer
{
    private static AmongUsRoomPlayer myRoomPlayer;

    public static AmongUsRoomPlayer MyRoomPlayer
    {
        get
        {
            if (myRoomPlayer == null)
            {
                var players = FindObjectsOfType<AmongUsRoomPlayer>();
                foreach (var player in players)
                {
                    if (player.hasAuthority)
                    {
                        myRoomPlayer = player;
                    }
                }
            }
            return myRoomPlayer;
        }
    }


    [SyncVar(hook = nameof(SetPlayerColor_Hook))]
    public EPlayerColor playerColor;

    public void SetPlayerColor_Hook(EPlayerColor oldColor, EPlayerColor newColr)
    {
        LobbyUIManager.Instance.CustomizeUI.UpdateUnselectColorButton(oldColor);
        LobbyUIManager.Instance.CustomizeUI.UpdateSelectColorButton(newColr);
    }

    [SyncVar]
    public string nickname;

    public CharacterMover myCharacter;

    public void Start()
    {
        base.Start();

        if (isServer)
        {
            SpawnLobbyPlayerCharacter();
            LobbyUIManager.Instance.ActiveStartButton();
        }

        if (isLocalPlayer)
        {
            CmdSetNickname(PlayerSettings.nickname);
        }

        LobbyUIManager.Instance.GameRoomPlayerCount.UpdatePlayerCount();
    }

    private void OnDestroy()
    {
        if (LobbyUIManager.Instance != null)
        {
            LobbyUIManager.Instance.GameRoomPlayerCount.UpdatePlayerCount();
            LobbyUIManager.Instance.CustomizeUI.UpdateUnselectColorButton(playerColor);
        }
    }

    [Command]
    public void CmdSetNickname(string nick)
    {
        nickname = nick;
        myCharacter.nickname = nick;
    }

    [Command]
    public void CmdSetPlayerColor(EPlayerColor color)
    {
        playerColor = color;
        myCharacter.playerColor = color;
    }

    private void SpawnLobbyPlayerCharacter()
    {
        // Utilisez votre manager pour obtenir la position de spawn
        var spawnPositions = FindObjectOfType<SpawnPositions>();
        Vector3 spawnPos = spawnPositions.GetSpawnPosition(); // position actuelle de spawn

        // Instanciez le personnage à partir du prefab
        var playerCharacter = Instantiate(NetworkManager.singleton.spawnPrefabs[0], spawnPos, Quaternion.identity).GetComponent<LobbyCharacterMover>();

        // Configurez l'échelle si nécessaire (ex : pour la symétrie de placement)
        playerCharacter.transform.localScale = spawnPositions.Index < 5 ? new Vector3(0.5f, 0.5f, 1f) : new Vector3(-0.5f, 0.5f, 1f);

        // Faites l'apparition du joueur côté serveur pour synchroniser avec le client
        NetworkServer.Spawn(playerCharacter.gameObject, connectionToClient);

        // Associez les informations du joueur
        playerCharacter.ownerNetId = netId;
        playerCharacter.playerColor = playerColor;
    }


    // private void SpawnLobbyPlayerCharacter()
    // {
    //     var roomSlots = (NetworkManager.singleton as AmongUsRoomManager).roomSlots;
    //     EPlayerColor color = EPlayerColor.Red;
    //     for(int i=0; i<(int)EPlayerColor.Lime + 1; i++)
    //     {
    //         bool isFindSameColor = false;
    //         foreach(var roomPlayer in roomSlots)
    //         {
    //             var amongUsRoomPlayer = roomPlayer as AmongUsRoomPlayer;
    //             if(amongUsRoomPlayer.playerColor == (EPlayerColor)i && roomPlayer.netId != netId)
    //             {
    //                 isFindSameColor = true;
    //                 break;
    //             }
    //         }

    //         if(!isFindSameColor)
    //         {
    //             color = (EPlayerColor)i;
    //             break;
    //         }
    //     }
    //     playerColor = color;

    //     var spawnPositions = FindObjectOfType<SpawnPositions>();
    //     int index = spawnPositions.Index;
    //     Vector3 spawnPos = spawnPositions.GetSpawnPosition();

    //     var playerCharacter = Instantiate(AmongUsRoomManager.singleton.spawnPrefabs[0], spawnPos, Quaternion.identity).GetComponent<LobbyCharacterMover>();
    //     playerCharacter.transform.localScale = index < 5 ? new Vector3(0.5f, 0.5f, 1f) : new Vector3(-0.5f, 0.5f, 1f);
    //     NetworkServer.Spawn(playerCharacter.gameObject, connectionToClient);
    //     playerCharacter.ownerNetId = netId;
    //     playerCharacter.playerColor = color;
    // }
}
