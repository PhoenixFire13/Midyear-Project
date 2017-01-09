using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MidyearProject.StageSelection
{
    public class StageSelection : MonoBehaviour
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
            SceneManager.LoadSceneAsync("CharacterSelection");
        }

        void LoadMatch()
        {
            Debug.Log("Match Start");
            // SceneManager.LoadSceneAsync("GameStart");
        }

        #endregion
    }
}