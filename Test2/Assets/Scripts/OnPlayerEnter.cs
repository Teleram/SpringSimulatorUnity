using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OnPlayerEnter : NetworkBehaviour {
    
    public int myPlayerId;

    private CentralSpawnScript centralSpawnScript;

    private GameMasterScript gameMasterScript;

    public bool amIAi;
    [SyncVar]
    private bool decidedIfAi;

    void Start()
    {
        decidedIfAi = false;

        centralSpawnScript = GetComponent<CentralSpawnScript>();

        GameObject gameMaster = GameObject.Find("GameMaster");
        gameMasterScript = gameMaster.GetComponent<GameMasterScript>();

        centralSpawnScript.gameMasterScript = gameMasterScript;

        myPlayerId = gameMasterScript.getIdForNewPlayer();

        if (isLocalPlayer)
        {
            centralSpawnScript.SpawnMainUnit(myPlayerId);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer && !decidedIfAi && gameMasterScript.gameHasStarted())
        {
            if(Input.GetKeyDown(KeyCode.Y))
            {
                decidedIfAi = true;
                gameMasterScript.allDecidedOnAi[myPlayerId] = true;
                amIAi = true;
                CmdCommunicateDecided();
            }

            if(Input.GetKeyDown(KeyCode.N))
            {
                decidedIfAi = true;
                gameMasterScript.allDecidedOnAi[myPlayerId] = true;
                amIAi = false;
                CmdCommunicateDecided();
            }
        }

    }

    [Command]
    private void CmdCommunicateDecided()
    {
        decidedIfAi = true;
        gameMasterScript.allDecidedOnAi[myPlayerId] = true;
        RpcCommunicateDecided();
    }

    [ClientRpc]
    private void RpcCommunicateDecided()
    {
        decidedIfAi = true;
        gameMasterScript.allDecidedOnAi[myPlayerId] = true;
    }
}
