using UnityEngine;
using System.Collections;

public class PlayerManager : Photon.PunBehaviour {

    GameObject playerInstance;
    //SkinnedMeshRenderer playerMesh;
    Transform[] spawnPoints;

    public Material[] playerColors;
    public GameObject camera;

    void Start () {
        int charColor = 0;
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("Spawn");
        spawnPoints = new Transform[spawns.Length];

        for (int i = 0; i < spawns.Length; i++)
        {
            spawnPoints[i] = spawns[i].transform;
        }

        if (PhotonNetwork.player.CustomProperties.ContainsKey("characterNum"))
        {
            charColor = (int)PhotonNetwork.player.CustomProperties["characterNum"];
        }

        Transform spawn = RandomSpawnPoint();
        playerInstance = PhotonNetwork.Instantiate("Kirby", spawn.position, spawn.rotation, 0);
        initializeCharacter(PhotonNetwork.player, playerInstance, charColor);
        playerInstance.GetComponent<CharacterMovement>().enabled = true;
        playerInstance.GetComponent<PlayerAttack>().enabled = true;
        GameObject playerCam = (GameObject)Instantiate(camera, playerInstance.transform);
        playerCam.transform.localPosition = new Vector3(0f, 7.5f, -10f);
        playerCam.transform.rotation = Quaternion.Euler(30f, 0f, 0f);
    }

    // Debugging purposes
    void Update()
    {
        //ChangeColor();
    }

    [PunRPC]
    private void initializeCharacter(PhotonPlayer p, GameObject obj , int n)
    {
        SkinnedMeshRenderer meshRenderer = obj.GetComponentInChildren<SkinnedMeshRenderer>();
        meshRenderer.materials = SetColor(n);
        if (p.CustomProperties.ContainsKey("playerNum"))
        {
            obj.tag = "Player " + (int)p.CustomProperties["playerNum"];
        }
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

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            Debug.Log("Sender Writing: " + info.sender.NickName + " ViewID: " + playerInstance.GetPhotonView().viewID + " Character: " + (int)PhotonNetwork.player.CustomProperties["characterNum"]);
            stream.SendNext((int)PhotonNetwork.player.CustomProperties["characterNum"]);
            stream.SendNext(playerInstance.GetPhotonView().viewID);
        }
        else
        {
            int character = (int)stream.ReceiveNext();
            int ID = (int)stream.ReceiveNext();
            Debug.Log("Sender Reading: " + info.sender.NickName + " ViewID: " + ID + " Character: " + character);
            initializeCharacter(info.sender, PhotonView.Find(ID).gameObject, character);
        }
    }

    // Debugging purposes
    //private void ChangeColor()
    //{
    //    if (Input.GetKeyDown(KeyCode.Keypad0))
    //        playerMesh.materials = SetColor(0);
    //    if (Input.GetKeyDown(KeyCode.Keypad1))
    //        playerMesh.materials = SetColor(1);
    //    if (Input.GetKeyDown(KeyCode.Keypad2))
    //        playerMesh.materials = SetColor(2);
    //    if (Input.GetKeyDown(KeyCode.Keypad3))
    //        playerMesh.materials = SetColor(3);
    //}

    //public override void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
    //{
    //    PhotonPlayer p = playerAndUpdatedProps[0] as PhotonPlayer;
    //    Debug.Log("PlayerManager: OnPhotonPlayerPropertiesChanged() was called by " + p.NickName);
    //    ExitGames.Client.Photon.Hashtable properties = playerAndUpdatedProps[1] as ExitGames.Client.Photon.Hashtable;
    //    if (!properties.ContainsKey("ready"))
    //    {
    //        return;
    //    }
    //    if (p.Equals(PhotonNetwork.player))
    //    {
    //        return;
    //    }
    //    Debug.Log("PlayerManager: OnPhotonPlayerPropertiesChanged() was not called by this player");
    //    GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
    //    if (objs.Length > 0)
    //    {
    //        initializeCharacter(p, objs[0], (int)p.CustomProperties["characterNum"]);
    //    }
    //}

}
