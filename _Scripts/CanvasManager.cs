using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasManager : Photon.PunBehaviour {

    public GameObject playerDamageMeter;
    public GameObject opponentDamageMeter;

    void Awake() {
        
    }

    public GameObject GetPlayerDMGMeter() {
        return playerDamageMeter;
    }

    public GameObject GetOpponentDMGMeter() {
        return opponentDamageMeter;
    }

    public Text GetPlayerDMGText()
    {
        Text[] texts = playerDamageMeter.GetComponentsInChildren<Text>();
        foreach (Text txt in texts)
        {
            if (txt.name == "DamageText")
            {
                return txt;
            }
        }

        return null;
    }

    public Text GetOpponentDMGText()
    {
        Text[] texts = opponentDamageMeter.GetComponentsInChildren<Text>();
        foreach (Text txt in texts)
        {
            if (txt.name == "DamageText")
                return txt;
        }

        return null;
    }

}
