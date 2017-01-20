using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MatchStartUI : MonoBehaviour {

    Animator canvasAnim;
    int countdown;

    public Text gameStartText;
    public Text countdownText;
    public Text goText;
    public float startCountDelay;
    public float countdownDelay;
    public float goBlink;

    void OnEnable()
    {
        gameStartText.enabled = true;
        countdownText.enabled = true;
        goText.enabled = false;

        canvasAnim = GetComponentInParent<Animator>();

        countdown = 3;
        SetCDText();

        StartCoroutine(CountDown());
    }

    void OnDisable()
    {
        gameStartText.enabled = false;
        countdownText.enabled = false;
        goText.enabled = false;
    }

    void SetCDText()
    {
        countdownText.text = countdown + "";
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(startCountDelay);
        canvasAnim.Play("MatchStart");   // Create condition where players have to be present for game to start

        while (countdown > 0)
        {
            yield return new WaitForSeconds(countdownDelay);

            countdown--;
            SetCDText();
        }

        canvasAnim.Stop();
        gameStartText.enabled = false;
        countdownText.enabled = false;

        goText.enabled = true;
        yield return new WaitForSeconds(goBlink);
        goText.enabled = false;
    }

}
