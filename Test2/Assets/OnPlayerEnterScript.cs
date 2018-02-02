using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OnPlayerEnterScript : NetworkBehaviour {

    public GameObject mainUnitPrefab;

	// Use this for initialization
	void Start () {
        // is this me?
        if(!isLocalPlayer)
        {
            return;
        }

        Debug.Log("I spawned!");

        CmdSpawnMainUnit();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    [Command]
    void CmdSpawnMainUnit()
    {
        GameObject tmp = Instantiate(mainUnitPrefab);

        NetworkServer.SpawnWithClientAuthority(tmp, connectionToClient);
    }
}
