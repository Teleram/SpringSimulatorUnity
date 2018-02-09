using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CentralSpawnScript : NetworkBehaviour {

    public GameObject mainUnit;

    public GameObject unit1;
    public Vector3 spawnpos1;

    [Command]
    public void CmdSpawnMainUnit()
    {
        GameObject tmp = Instantiate(mainUnit);

        NetworkServer.SpawnWithClientAuthority(tmp, connectionToClient);
        //NetworkServer.Spawn(tmp);

        Spawn localSpawnScript = (Spawn)tmp.GetComponent("Spawn");
        localSpawnScript.centralSpawnScript = this;
    }

    [Command]
    public void CmdSpawnUnit()
    {
        GameObject tmp = Instantiate(unit1);

        NetworkServer.SpawnWithClientAuthority(tmp, connectionToClient);
        //NetworkServer.Spawn(tmp);

        tmp.transform.position = spawnpos1;
    }
}
