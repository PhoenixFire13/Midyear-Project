using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CharacterSelection : MonoBehaviour {

    public Button nextButton;
    public Button backButton;

    // Use this for initialization
    void Start()
    {
        nextButton.onClick.AddListener(LoadStageSelectionScene);
        backButton.onClick.AddListener(LoadGameMenu);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LoadGameMenu()
    {
        SceneManager.LoadSceneAsync("GameStartMenu");
    }

    void LoadStageSelectionScene()
    {
        Debug.Log("Stage Selection");
        // SceneManager.LoadSceneAsync("StageSelection");
    }
}
