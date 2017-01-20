using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MatchEndUI : MonoBehaviour {

    Animator canvasAnim;

    public Text gameEndText;
    public Text winnerText;

    void OnEnable()
    {
        gameEndText.enabled = true;
        winnerText.enabled = true;
    }

    void OnDisable()
    {
        gameEndText.enabled = false;
        winnerText.enabled = false;
    }

}
