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
            nextButton.onClick.AddListener(LoadMatch);
            backButton.onClick.AddListener(LoadCharacterSelectionScene);
        }

        #endregion


        #region Private Methods

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
            // SceneManager.LoadSceneAsync("GameStart");
        }

        #endregion

        #region Photon.PunBehaviour Call Backs

        public override void OnLeftRoom()
        {
            Debug.Log("StageSelection: OnLeftRoom() was called by PUN");
            SceneManager.LoadScene("GameLobby");
        }

        public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
        {
            PhotonNetwork.LoadLevel("CharacterSelection");
        }

        #endregion
    }
}