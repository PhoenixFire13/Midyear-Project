using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public Button startGameButton;
    public Button controlsButton;

	// Use this for initialization
	void Start () {
        startGameButton.onClick.AddListener(LoadCharacterSelectionScene);
        controlsButton.onClick.AddListener(LoadControlsScene);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void LoadCharacterSelectionScene() {
        SceneManager.LoadSceneAsync("CharacterSelection");
    }

    void LoadControlsScene(){
        SceneManager.LoadSceneAsync("Controls");
    }
}
