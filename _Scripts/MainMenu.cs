using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MidyearProject.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        #region Public fields

        public Button startGameButton;
        public Button controlsButton;

        #endregion


        #region MonoBehaviour CallBacks

        void Start()
        {
            startGameButton.onClick.AddListener(LoadGameLobbyScene);
            controlsButton.onClick.AddListener(LoadControlsScene);
        }

        #endregion


        #region Private Methods

        void LoadGameLobbyScene()
        {
            SceneManager.LoadSceneAsync("GameLobby");
        }

        void LoadControlsScene()
        {
            SceneManager.LoadSceneAsync("Controls");
        }

        #endregion
    }
}