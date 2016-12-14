using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class StageSelection : MonoBehaviour
{

    public Button nextButton;
    public Button backButton;

    // Use this for initialization
    void Start()
    {
        nextButton.onClick.AddListener(LoadMatch);
        backButton.onClick.AddListener(LoadCharacterSelectionScene);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LoadCharacterSelectionScene()
    {
        SceneManager.LoadSceneAsync("characterSelection");
    }

    void LoadMatch()
    {
        Debug.Log("Match Start");
        // SceneManager.LoadSceneAsync("GameStart");
    }
}