using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class CreateRoomUI : MonoBehaviour
{
    //[SerializeField]
    //private List<Image> crewImgs;

    // [SerializeField]
    // private List<Button> impostersCountButtons;

    // [SerializeField]
    // private List<Button> maxPlayerCountButtons;

    private CreateGameRoomData roomData;

    // Start is called before the first frame update
    void Start()
    {
        // for (int i = 0; i < crewImgs.Count; i++)
        // {
        //     Material materialInstance = Instantiate(crewImgs[i].material);
        //     crewImgs[i].material = materialInstance;
        // }

        roomData = new CreateGameRoomData() { imposterCount = 1, maxPlayerCount = 4 };
        
        //UpdateCrewImages();
    }

    // public void UpdateImposterCount(int count)
    // {
    //     roomData.imposterCount = count;

    //     for (int i = 0; i < impostersCountButtons.Count; i++)
    //     {
    //         if (i == count - 1)
    //         {
    //             impostersCountButtons[i].image.color = new Color(1f, 1f, 1f, 1f);
    //         }
    //         else
    //         {
    //             impostersCountButtons[i].image.color = new Color(1f, 1f, 1f, 0f);
    //         }
    //     }

    //     int limitMaxPlayer = 3; //count == 1 ? 4 : count == 2 ? 7 : 9;
    //     if(roomData.maxPlayerCount < limitMaxPlayer)
    //     {
    //         UpdateMaxPlayerCount(limitMaxPlayer);
    //     }
    //     else
    //     {
    //         UpdateMaxPlayerCount(roomData.maxPlayerCount);
    //     }

    //     for(int i=0; i< maxPlayerCountButtons.Count; i++)
    //     {
    //         var text = maxPlayerCountButtons[i].GetComponentInChildren<Text>();
    //         if(i < limitMaxPlayer - 3)
    //         {
    //             maxPlayerCountButtons[i].interactable = false;
    //             text.color = Color.gray;
    //         }
    //         else
    //         {
    //             maxPlayerCountButtons[i].interactable = true;
    //             text.color = Color.white;
    //         }
    //     }
    // }

    // public void UpdateMaxPlayerCount(int count)
    // {
    //     roomData.maxPlayerCount = count;

    //     for(int i=0; i< maxPlayerCountButtons.Count; i++)
    //     {
    //         if(i == count - 3)
    //         {
    //             maxPlayerCountButtons[i].image.color = new Color(1f, 1f, 1f, 1f);
    //         }
    //         else
    //         {
    //             maxPlayerCountButtons[i].image.color = new Color(1f, 1f, 1f, 0f);
    //         }
    //     }

    //     UpdateCrewImages();
    // }

    // private void UpdateCrewImages()
    // {
    //     for (int i = 0; i < crewImgs.Count; i++)
    //     {
    //         crewImgs[i].material.SetColor("_PlayerColor", Color.white);
    //     }

    //     int imposterCount = roomData.imposterCount;
    //     int idx = 0;
    //     while (imposterCount != 0)
    //     {
    //         if (idx >= roomData.maxPlayerCount)
    //         {
    //             idx = 0;
    //         }

    //         if (crewImgs[idx].material.GetColor("_PlayerColor") != Color.red && Random.Range(0, 5) == 0)
    //         {
    //             crewImgs[idx].material.SetColor("_PlayerColor", Color.red);
    //             imposterCount--;
    //         }
    //         idx++;
    //     }

    //     for (int i = 0; i < crewImgs.Count; i++)
    //     {
    //         if (i < roomData.maxPlayerCount)
    //         {
    //             crewImgs[i].gameObject.SetActive(true);
    //         }
    //         else
    //         {
    //             crewImgs[i].gameObject.SetActive(false);
    //         }
    //     }
    // }

    public void CreateRoom()
    {
        var manager = NetworkManager.singleton as AmongUsRoomManager;

        manager.minPlayerCount = 3; //roomData.imposterCount == 1 ? 4 : roomData.imposterCount == 2 ? 7 : 9;
        manager.imposterCount = 1; //roomData.imposterCount;
        manager.maxConnections = 4; //roomData.maxPlayerCount;
        manager.StartHost();
    }

    public class CreateGameRoomData
    {
        public int imposterCount;
        public int maxPlayerCount;
    }
}
