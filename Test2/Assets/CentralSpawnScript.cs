using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CentralSpawnScript : NetworkBehaviour {

    public GameObject mainUnit;

    //public GameObject unit1;
    //public Vector3 spawnpos1;
    private GameObject nextUnit;
    private Vector3 nextSpawnpos;


    // Spawns the Main Unit
    [Command]
    public void CmdSpawnMainUnit()
    {
        GameObject tmp = Instantiate(mainUnit);

        NetworkServer.SpawnWithClientAuthority(tmp, connectionToClient);
        //NetworkServer.Spawn(tmp);

        Spawn localSpawnScript = (Spawn)tmp.GetComponent("Spawn");
        localSpawnScript.centralSpawnScript = this;
    }

    // Puts Parameters into Variables, because CmdSpawnUnit doesn´t take the Parameters directly
    public void SpawnUnit(Vector3 spawnpos, GameObject unittype)
    {
        nextSpawnpos = spawnpos;
        nextUnit = unittype;
        CmdSpawnUnit();
    }

    // Spawns a normal Unit
    [Command]
    private void CmdSpawnUnit()
    {
        GameObject tmp = Instantiate(nextUnit);

        NetworkServer.SpawnWithClientAuthority(tmp, connectionToClient);
        //NetworkServer.Spawn(tmp);

        tmp.transform.position = nextSpawnpos;
    }
}
