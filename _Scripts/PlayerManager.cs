using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    GameObject playerInstance;
    SkinnedMeshRenderer playerMesh;

    public GameObject player;
    public Transform[] spawnPoints;
    public Material[] playerColors;

    void Start () {
        playerInstance = Instantiate(player);

        playerMesh = playerInstance.GetComponentInChildren<SkinnedMeshRenderer>();
        playerMesh.materials = SetColor(0);   // --------------- Change conditions to set color to what player selects ---------------

        Transform spawn = RandomSpawnPoint();
        playerInstance.transform.position = spawn.position;
        playerInstance.transform.rotation = spawn.rotation;
	}

    // Debugging purposes
    void Update()
    {
        ChangeColor();
    }

    private Transform RandomSpawnPoint()
    {
        int random = Random.Range(0, spawnPoints.Length);
        return spawnPoints[random];
    }

    private Material[] SetColor (int index)
    {
        Material[] color = new Material[2];
        color[0] = playerColors[index];
        color[1] = playerColors[index];

        return color;
    }

    // Debugging purposes
    private void ChangeColor()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
            playerMesh.materials = SetColor(0);
        if (Input.GetKeyDown(KeyCode.Keypad1))
            playerMesh.materials = SetColor(1);
        if (Input.GetKeyDown(KeyCode.Keypad2))
            playerMesh.materials = SetColor(2);
        if (Input.GetKeyDown(KeyCode.Keypad3))
            playerMesh.materials = SetColor(3);
    }

}
