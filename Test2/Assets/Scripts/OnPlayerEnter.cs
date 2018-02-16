using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OnPlayerEnter : NetworkBehaviour {
    
    private int myPlayerId;

    public CentralSpawnScript centralSpawnScript;

    // Use this for initialization
    void Start () {

        GameObject gameMaster = GameObject.Find("GameMaster");
        GameMasterScript gameMasterScript = (GameMasterScript) gameMaster.GetComponent("GameMasterScript");
        centralSpawnScript.gameMasterScript = gameMasterScript;

        myPlayerId = gameMasterScript.getIdForNewPlayer();

        // is this me?
        if (isLocalPlayer)
        {
            Debug.Log("I spawned!");

            centralSpawnScript.SpawnMainUnit(myPlayerId);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    

}
