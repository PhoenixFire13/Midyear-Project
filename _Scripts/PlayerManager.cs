using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    GameObject playerInstance;

    public GameObject player;
    public Transform[] spawnPoints;

    void Awake () {
        playerInstance = Instantiate(player);

        Transform spawn = RandomSpawnPoint();
        playerInstance.transform.position = spawn.position;
        playerInstance.transform.rotation = spawn.rotation;
	}

    private Transform RandomSpawnPoint()
    {
        int random = Random.Range(0, spawnPoints.Length);
        return spawnPoints[random];
    }

}
