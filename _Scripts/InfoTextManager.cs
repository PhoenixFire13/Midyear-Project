using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MidyearProject.InfoTextManager
{
    public class InfoTextManager : MonoBehaviour
    {
        #region Public fields

        public Button backButton;
        public Button nextButton;
        public GameObject[] texts;

        #endregion


        #region Private fields

        private int currentText;

        #endregion


        #region MonoBehaviour CallBacks

        void Start()
        {
            currentText = 0;
            texts[currentText].SetActive(true);   // default page

            backButton.onClick.AddListener(Back);
            nextButton.onClick.AddListener(Next);
        }

        void Update()
        {
            // Changes Next button text to Start Game on last page
            if (currentText >= texts.Length - 1)
                nextButton.GetComponentInChildren<Text>().text = "Main Menu";
            else
                nextButton.GetComponentInChildren<Text>().text = "Next";
        }

        #endregion


        #region Private Methods

        void Back()
        {
            if (currentText == 0)
            {
                SceneManager.LoadSceneAsync("GameStartMenu");
                return;
            }

            texts[currentText].SetActive(false);
            currentText--;
            texts[currentText].SetActive(true);
        }

        void Next()
        {
            if (currentText == texts.Length - 1)
            {
                SceneManager.LoadSceneAsync("GameStartMenu");
                return;
            }

            texts[currentText].SetActive(false);
            currentText++;
            texts[currentText].SetActive(true);
        }

        #endregion
    }
}
