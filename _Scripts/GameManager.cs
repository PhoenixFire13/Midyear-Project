using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

namespace Midyear.GameManager
{
    public class GameManager : MonoBehaviour
    {


        #region Photon Messages


        /// <summary>
        /// Called when the local player left the room. We need to load the launcher scene.
        /// </summary>
        public void OnLeftRoom()
        {
            SceneManager.LoadSceneAsync("LobbyScene");
        }


        #endregion


        #region Public Methods


        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }


        #endregion
    }
}