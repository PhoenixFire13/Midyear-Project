using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CreditsManager : MonoBehaviour {

    AudioSource aud;

    public GameObject creditsEnd;
    public float audioDelay;
    public float endingDelay;

    void Start()
    {
        aud = GetComponent<AudioSource>();
        StartCoroutine(PlayBGM());
    }

    void Update()
    {
        if (creditsEnd.activeSelf)
            StartCoroutine(ReturnToMenu());
    }

    IEnumerator PlayBGM()
    {
        yield return new WaitForSeconds(audioDelay);
        aud.Play();
    }

    IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(endingDelay);
        SceneManager.LoadSceneAsync("GameStartMenu");
    }

}
