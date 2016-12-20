using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MidyearProject.CharacterSelection
{
    public class CharacterSelection : MonoBehaviour
    {
        #region Public fields

        public Button nextButton;
        public Button backButton;

        #endregion


        #region MonoBehaviour CallBacks

        void Start()
        {
            nextButton.onClick.AddListener(LoadStageSelectionScene);
            backButton.onClick.AddListener(LoadGameMenu);
        }

        #endregion


        #region Private Methods

        void LoadGameMenu()
        {
            SceneManager.LoadSceneAsync("GameStartMenu");
        }

        void LoadStageSelectionScene()
        {
            SceneManager.LoadSceneAsync("StageSelection");
        }

        #endregion
    }
}