using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class InfoTextManager : MonoBehaviour {

    public Button backButton;
    public Button nextButton;
    public GameObject[] texts;

    private int currentText;

	// Use this for initialization
	void Start () {
        currentText = 0;
        texts[currentText].SetActive(true);   // default page

        backButton.onClick.AddListener(Back);
        nextButton.onClick.AddListener(Next);
	}

    void Update() {
        // Changes Next button text to Start Game on last page
        if (currentText >= texts.Length - 1)
            nextButton.GetComponentInChildren<Text>().text = "Main Menu";
        else
            nextButton.GetComponentInChildren<Text>().text = "Next";
    }

    void Back() {
        if (currentText == 0)
            SceneManager.LoadSceneAsync("GameStartMenu");

        texts[currentText].SetActive(false);
        currentText--;
        texts[currentText].SetActive(true);
    }

    void Next() {
        if (currentText == texts.Length-1)
            SceneManager.LoadSceneAsync("GameStartMenu");

        texts[currentText].SetActive(false);
        currentText++;
        texts[currentText].SetActive(true);
    }
}
