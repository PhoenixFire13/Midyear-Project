using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MidyearProject.StageSelection
{
    public class StageSelection : Photon.PunBehaviour
    {
        #region Public fields

        public Button nextButton;
        public Button backButton;

        #endregion


        #region MonoBehaviour CallBacks

        void Start()
        {
            if (!PhotonNetwork.player.IsMasterClient)
            {
                nextButton.gameObject.SetActive(false);
            }

            makeButtonActive();
            backButton.onClick.AddListener(LoadCharacterSelectionScene);
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the stageNum in the room properties so that the stage can be created with the selected stage
        /// </summary>
        /// 
        /// <param name="n"> 0: Mario Theme
        /// </param>
        public void setStageNum(int n)
        {
            PhotonNetwork.room.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "stageNum", n } });
            makeButtonActive();
        }

        #endregion

        #region Private Methods

        void makeButtonActive()
        {
            if (PhotonNetwork.room.CustomProperties.ContainsKey("stageNum"))
            {
                nextButton.onClick.AddListener(LoadMatch);
            }
            else
            {
                nextButton.onClick.RemoveAllListeners();
            }
        }

        void LoadCharacterSelectionScene()
        {
            if (PhotonNetwork.player.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("CharacterSelection");
            }
            else
            {
                PhotonNetwork.LeaveRoom();
            }
        }

        void LoadMatch()
        {
            Debug.Log("Match Start");
            PhotonNetwork.LoadLevel("Stage");
        }

        #endregion

        #region Photon.PunBehaviour Call Backs

        public override void OnLeftRoom()
        {
            Debug.Log("StageSelection: OnLeftRoom() was called by PUN");
            PhotonNetwork.player.CustomProperties.Clear();
            SceneManager.LoadScene("GameLobby");
        }

        public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
        {
            PhotonNetwork.LoadLevel("CharacterSelection");
        }

        #endregion
    }
}