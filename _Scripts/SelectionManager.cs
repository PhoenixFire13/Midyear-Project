using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MidyearProject.SelectionManager
{
    public class SelectionManager : Photon.PunBehaviour
    {
        #region Public variables
        public Text[] players;

        #endregion

        #region Private variables
        private Button[] characters;

        #endregion

        #region Public methods

        /// <summary>
        /// Set the image on the character selection stage to the selected character
        /// </summary>
        /// <param name="n"> 0: Pink
        ///                  1: Blue
        ///                  2: Green
        ///                  3: Yellow
        ///                  4: Random
        /// </param>
        public void setCharacter(int n)
        {
            int playerIndex;
            if ((int)PhotonNetwork.player.CustomProperties["playerNum"] == 1)
            {
                playerIndex = 0;
            }
            else if ((int)PhotonNetwork.player.CustomProperties["playerNum"] == 2)
            {
                playerIndex = 1;
            }
            else
            {
                return;
            }

            players[playerIndex].GetComponentInParent<RawImage>().texture = characters[n].gameObject.GetComponent<RawImage>().texture;
            players[playerIndex].GetComponentInParent<RawImage>().color = Color.white;
            PhotonNetwork.player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "characterNum", n } });
        }

        #endregion

        #region Private methods
        private void setCharacter(PhotonPlayer p, int n)
        {
            int playerIndex;
            if ((int)p.CustomProperties["playerNum"] == 1)
            {
                playerIndex = 0;
            }
            else if ((int)p.CustomProperties["playerNum"] == 2)
            {
                playerIndex = 1;
            }
            else
            {
                return;
            }

            players[playerIndex].GetComponentInParent<RawImage>().texture = characters[n].gameObject.GetComponent<RawImage>().texture;
            players[playerIndex].GetComponentInParent<RawImage>().color = Color.white;
        }

        #endregion

        #region MonoBehaviour Call Backs

        // Use this for initialization
        void Start()
        {
            Debug.Log("Selection Manager: Start()");
            characters = GetComponentsInChildren<Button>();
            foreach (PhotonPlayer player in PhotonNetwork.playerList)
            {
                if (player.CustomProperties.ContainsKey("characterNum"))
                    setCharacter(player, (int)player.CustomProperties["characterNum"]);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion

        #region PunBehaviour Call Backs
        public override void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
        {
            Debug.Log("SelectionManager: OnPhotonPlayerPropertiesChanged() was called by PUN");
            if ((playerAndUpdatedProps[1] as ExitGames.Client.Photon.Hashtable).ContainsKey("playerNum"))
            {
                Start();
                return;
            }
            Debug.Log("Player Name: " + (playerAndUpdatedProps[0] as PhotonPlayer).NickName);
            Debug.Log("Property Value: " + (int)(playerAndUpdatedProps[1] as ExitGames.Client.Photon.Hashtable)["characterNum"]);
            PhotonPlayer player = playerAndUpdatedProps[0] as PhotonPlayer;
            ExitGames.Client.Photon.Hashtable property = playerAndUpdatedProps[1] as ExitGames.Client.Photon.Hashtable;
            foreach (Text text in players)
            {
                if (text.text.Equals(player.NickName) && property.ContainsKey("characterNum"))
                {
                    text.GetComponentInParent<RawImage>().texture = characters[(int)property["characterNum"]].GetComponent<RawImage>().texture;
                    text.GetComponentInParent<RawImage>().color = Color.white;
                }
            }
        }

        public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
        {
            Debug.Log("SelectionManager: OnPhotonPlayerDisconnected() was called by PUN \nPlayers :" + PhotonNetwork.playerList.Length);
            for (int i = 0; i < players.Length; i++)
            {
                Text txt = players[i];
                players[i].text = "Player " + (i + 1);
                txt.GetComponentInParent<RawImage>().texture = null;
                txt.GetComponentInParent<RawImage>().color = Color.clear;
            }

            foreach (PhotonPlayer player in PhotonNetwork.playerList)
            {
                if (player.IsMasterClient)
                {
                    if (player.CustomProperties.ContainsKey("playerNum") && (int)player.CustomProperties["playerNum"] != 1)
                    {
                        player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "playerNum", 1 } });
                    }
                }
                else
                {
                    if (player.CustomProperties.ContainsKey("playerNum") && (int)player.CustomProperties["playerNum"] != 2)
                    {
                        player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "playerNum", 2 } });
                    }
                }
            }

            foreach (PhotonPlayer player in PhotonNetwork.playerList)
            {
                Debug.Log("Player Name: " + player.NickName + "\nContains Character Number: " + player.CustomProperties.ContainsKey("characterNum"));
                if ((int)player.CustomProperties["playerNum"] == 1)
                {
                    players[0].text = player.NickName;
                }

                if ((int)player.CustomProperties["playerNum"] == 2)
                {
                    players[1].text = player.NickName;
                }

                if (player.CustomProperties.ContainsKey("characterNum"))
                {
                    setCharacter(player, (int)player.CustomProperties["characterNum"]);
                }
            }
        }

        #endregion
    }
}