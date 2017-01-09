using UnityEngine;
using UnityEngine.UI;

namespace Midyear.Launcher
{
    public class Launcher : Photon.PunBehaviour
    {
        #region Public Variables


        public PhotonLogLevel LogLevel = PhotonLogLevel.Informational;
        public byte maxPlayersPerRoom = 4;

        //Name panel    [0]
        //Bottom panel  [1]
        public GameObject[] panels;
        
        #endregion


        #region Private Variables


        /// <summary>
        /// This client's version number. Users are separated from each other by gameversion (which allows you to make breaking changes).
        /// </summary>
        private string gameVersion = "v1";
        private string roomName;
        private RoomInfo[] roomList;


        #endregion


        #region MonoBehaviour CallBacks


        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
        /// </summary>
        void Awake()
        {
            // #NotImportant
            // Force Full LogLevel
            PhotonNetwork.logLevel = LogLevel;

            // #Critical
            // we don't join the lobby. There is no need to join a lobby to get the list of rooms.
            PhotonNetwork.autoJoinLobby = false;

            // #Critical
            // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
            PhotonNetwork.automaticallySyncScene = true;

            // Sets the name panel visible and the bottom panel invisible if client is no connected to photon else, the bottom panel is visible and name panel is not
            if (!PhotonNetwork.connected)
            {
                panels[0].SetActive(true);
                panels[1].SetActive(false);
            }
            else
            {
                panels[1].SetActive(true);
                panels[0].SetActive(false);
            }
        }


        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase.
        /// </summary>
        void Start()
        {
            roomName = "";
        }


        #endregion


        #region Public Methods


        /// <summary>
        /// Start the connection process. 
        /// - if not yet connected, Connect this application instance to Photon Cloud Network
        /// </summary>
        public void ConnectToMaster()
        {
            if (!PhotonNetwork.connected)
            {
                PhotonNetwork.ConnectUsingSettings(gameVersion);

            }
            panels[1].SetActive(true);
            panels[0].SetActive(false);
        }

        /// <summary>
        /// Connect to a random room, a room using the name, or a new room once connected to lobby
        /// </summary>
        public void RandomConnect()
        {
            if (PhotonNetwork.connected && PhotonNetwork.insideLobby)
            {
                PhotonNetwork.JoinRandomRoom();
                panels[0].SetActive(false);
                panels[1].SetActive(false);
            }
        }

        public void ConnectToRoom()
        {
            if (PhotonNetwork.connected && PhotonNetwork.insideLobby)
            {
                PhotonNetwork.JoinRoom(roomName);
                panels[0].SetActive(false);
                panels[1].SetActive(false);
            }
        }

        public void CreateNewRoom()
        {
            if (PhotonNetwork.connected && PhotonNetwork.insideLobby)
            {
                PhotonNetwork.CreateRoom(roomName, new RoomOptions() { MaxPlayers = maxPlayersPerRoom, PublishUserId = true }, TypedLobby.Default);
                panels[0].SetActive(false);
                panels[1].SetActive(false);
            }
        }

        public void updateRoomName(string value)
        {
            roomName = value;
        }


        #endregion


        #region Photon.PunBehavior CallBacks


        public override void OnConnectedToMaster()
        {
            Debug.Log("Launcher: OnConnectedToMaster() was called by PUN");
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            Debug.Log("Launcher: OnJoinedLobby() was called by PUN");
            roomList = PhotonNetwork.GetRoomList();
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Launcher: OnJoinedRoom() was called by PUN");
        }

        public override void OnCreatedRoom()
        {
            Debug.Log("Launcher: OnCreatedRoom() was called by PUN");
            PhotonNetwork.LoadLevel("CharacterSelection");
        }

        public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
        {
            Debug.Log("Launcher: OnPhotonPlayerConnected() was called by PUN \nPlayer: " + newPlayer.NickName);
        }

        public override void OnDisconnectedFromPhoton()
        {
            Debug.LogWarning("Launcher: OnDisconnectedFromPhoton() was called by PUN");
        }

        public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
        {
            Debug.Log("Launcher: OnPhotonRandomJoinFailed() was called by PUN");
        }

        public override void OnPhotonCreateRoomFailed(object[] codeAndMsg)
        {
            Debug.Log("Launcher: OnPhotonCreateRoomFailed was called by PUN");
        }

        public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
        {
            Debug.Log("Launcher: OnPhotonJoinRoomFailed was called by PUN");
        }


        #endregion

    }
}