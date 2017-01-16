using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MidyearProject.CharacterSelection
{
    public class CharacterSelection : Photon.PunBehaviour
    {
        #region Public fields

        public Button nextButton;
        public Button backButton;

        /* players: [0] = Player 1;
         *          [1] = Player 2;
         */
        public Text[] players;

        #endregion


        #region MonoBehaviour Call Backs

        void Start()
        {
            Debug.Log("Start was called by CharacterSelection");
            initializePlayerProperties();
        }

        #endregion


        #region Private Methods

        void LoadGameLobby()
        {
            PhotonNetwork.LeaveRoom();
        }

        void LoadStageSelectionScene()
        {
            PhotonNetwork.LoadLevel("StageSelection");
        }

        void makeButtonActive()
        {
            if (PhotonNetwork.playerList.Length == PhotonNetwork.room.MaxPlayers)
            {
                nextButton.onClick.AddListener(LoadStageSelectionScene);
            }
            else
            {
                nextButton.onClick.RemoveAllListeners();
            }
        }

        void initializePlayerProperties()
        {
            if (!PhotonNetwork.player.IsMasterClient)
            {
                nextButton.gameObject.SetActive(false);
            }

            makeButtonActive();
            backButton.onClick.AddListener(LoadGameLobby);

            foreach (PhotonPlayer player in PhotonNetwork.playerList)
            {
                if (player.IsMasterClient)
                {
                    if (!player.CustomProperties.ContainsKey("playerNum") || (int)player.CustomProperties["playerNum"] != 1)
                    {
                        player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "playerNum", 1 } });
                    }
                    players[0].text = player.NickName;
                }
                else
                {
                    if (!player.CustomProperties.ContainsKey("playerNum") || (int)player.CustomProperties["playerNum"] != 2)
                    {
                        player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "playerNum", 2 } });
                    }
                    players[1].text = player.NickName;
                }
            }
        }

        #endregion


        #region Photon.PunBehaviour Call Backs

        public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
        {
            Debug.Log("CharacterSelection: OnPhotonPlayerConnected() was called by PUN");
            Debug.Log(PhotonNetwork.playerList.Length);
            foreach (PhotonPlayer player in PhotonNetwork.playerList)
            {
                if (player.IsMasterClient)
                {
                    if (!player.CustomProperties.ContainsKey("playerNum") || (int)player.CustomProperties["playerNum"] != 1)
                    {
                        player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "playerNum", 1 } });
                    }
                    players[0].text = player.NickName;
                }
                else
                {
                    if (!player.CustomProperties.ContainsKey("playerNum") || (int)player.CustomProperties["playerNum"] != 2)
                    {
                        player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "playerNum", 2 } });
                    }
                    players[1].text = player.NickName;
                }
            }

            makeButtonActive();
        }

        public override void OnLeftRoom()
        {
            Debug.Log("CharacterSelection: OnLeftRoom() was called by PUN");
            PhotonNetwork.player.CustomProperties.Clear();
            SceneManager.LoadScene("GameLobby");
        }

        public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
        {
            Debug.Log("CharacterSelection: OnPhotonPlayerDisconnected() was called by PUN");
            Debug.Log(PhotonNetwork.player.IsMasterClient);

            makeButtonActive();
        }

        public override void OnMasterClientSwitched(PhotonPlayer newMasterClient)
        {
            Debug.Log("CharacterSelection: OnMasterClientSwitched() was called by PUN");
            nextButton.gameObject.SetActive(true);
        }

        #endregion
    }
}