using UnityEngine;
using System.Collections;

public class CanvasManager : Photon.PunBehaviour {

    GameObject playerMeter;
    GameObject opponentMeter;

    public GameObject playerDamageMeter;
    public GameObject opponentDamageMeter;

    void Awake() {
        playerMeter = Instantiate(playerDamageMeter);
        playerMeter.transform.SetParent(gameObject.transform);

        opponentMeter = Instantiate(opponentDamageMeter);
        opponentMeter.transform.SetParent(gameObject.transform);
    }

    public GameObject GetPlayerDMGMeter() {
        return playerMeter;
    }

    public GameObject GetOpponentDMGMeter() {
        return opponentMeter;
    }

}
