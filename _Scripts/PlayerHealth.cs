using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    private Text damageText;
    private int currentHP;

    public static int INITIAL_DAMAGE_VALUE = 0;
    public static int MAX_DAMAGE_VALUE = 999;

    void Awake()
    {
        Canvas healthCanvas = FindObjectOfType<Canvas>();
        damageText = healthCanvas.GetComponent<CanvasManager>().GetOpponentDMGText();

        currentHP = INITIAL_DAMAGE_VALUE;
    }

    public int GetCurrentHealth()
    {
        return currentHP;
    }

    public void TakeDamage (int amount)
    {
        if (currentHP < 999)
            currentHP += amount;
        else if (currentHP >= 999)
            currentHP = 999;

        SetHealthUI();
    }

    private void SetHealthUI()
    {
        damageText.text = currentHP + "%";
    }

}
