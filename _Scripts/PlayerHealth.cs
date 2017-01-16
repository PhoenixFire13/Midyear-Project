using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    private Text damageText;
    private float currentHP;

    public static int INITIAL_DAMAGE_VALUE = 0;
    public static int MAX_DAMAGE_VALUE = 999;

    void Awake()
    {
        Text[] texts = Resources.FindObjectsOfTypeAll<Text>();
        foreach (Text txt in  texts)
        {
            if (txt.tag == "OpponentHealth")
            {
                damageText = txt;
                break;
            }
        }

        currentHP = INITIAL_DAMAGE_VALUE;
    }

    // Debug purposes
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            TakeDamage(10);
        }
    }

    public float GetCurrentHealth()
    {
        return currentHP;
    }

    public void TakeDamage (int amount)
    {
        currentHP += amount;
        SetHealthUI();
    }

    private void SetHealthUI()
    {
        damageText.text = currentHP + "%";
    }

    // --------------- Death condition: Outside of stage boundaries (kill box) ---------------
    // Might move to separate script assigned to KillBox parent
    private void OnDeath()
    {

    }

}
