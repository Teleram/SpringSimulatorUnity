using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OnPlayerEnter : NetworkBehaviour
{

    public int myPlayerId;

    private CentralSpawnScript centralSpawnScript;

    private GameMasterScript gameMasterScript;

    public bool amIAi;
    [SyncVar]
    private bool decidedIfAi;

    // for ai training
    private float timer;

    void Start()
    {
        decidedIfAi = false;

        centralSpawnScript = GetComponent<CentralSpawnScript>();

        GameObject gameMaster = GameObject.Find("GameMaster");
        gameMasterScript = gameMaster.GetComponent<GameMasterScript>();

        centralSpawnScript.gameMasterScript = gameMasterScript;

        myPlayerId = gameMasterScript.GetIdForNewPlayer();

        if (isLocalPlayer)
        {
            centralSpawnScript.SpawnMainUnit(myPlayerId);
            // for ai training
            timer = 10.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // TimerReady exists just for ai training, disable all functionality related to it, if you want to play the game
        if (!TimerReady() && gameMasterScript.GameHasStarted())
        {
            timer -= Time.deltaTime;
        }
        //if (isLocalPlayer && !decidedIfAi && gameMasterScript.gameHasStarted())
        if (isLocalPlayer && !decidedIfAi && gameMasterScript.GameHasStarted() && TimerReady())
        {
            // the ai plays for you, activated for ai training
            decidedIfAi = true;
            gameMasterScript.allDecidedOnAi[myPlayerId] = true;
            amIAi = true;
            CmdCommunicateDecided();

            // if you want to play this game, this allows you to choose whether you want to play yourself or if you want the ai to play for you
            // disabled for ai training
            /*
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
            */
        }
    }

    // TimerReady exists just for ai training, disable all functionality related to it, if you want to play the game
    private bool TimerReady()
    {
        return timer <= 0.0f;
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
