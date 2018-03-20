using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerEnter_Singleplayer : MonoBehaviour
{

    public int myPlayerId;

    private CentralSpawnScript_Singleplayer centralSpawnScript;

    private GameMasterScript_Singleplayer gameMasterScript;

    public bool amIAi;

    private bool decidedIfAi;

    // for ai training
    //private float timer;

    void Start()
    {
        decidedIfAi = false;

        centralSpawnScript = GetComponent<CentralSpawnScript_Singleplayer>();
        
        GameObject gameMaster = GameObject.FindGameObjectWithTag("GameMaster");
        gameMasterScript = gameMaster.GetComponent<GameMasterScript_Singleplayer>();

        centralSpawnScript.gameMasterScript = gameMasterScript;

        myPlayerId = gameMasterScript.GetIdForNewPlayer();

        centralSpawnScript.SpawnMainUnit(myPlayerId);
        // for ai training
        //timer = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // TimerReady exists just for ai training, disable all functionality related to it, if you want to play the game
        //if (!TimerReady() && gameMasterScript.GameHasStarted())
        //{
        //    timer -= Time.deltaTime;
        //}

        //if (isLocalPlayer && !decidedIfAi && gameMasterScript.gameHasStarted())
        if (!decidedIfAi && gameMasterScript.GameHasStarted())
        {
            // the ai plays for you, activated for ai training
            decidedIfAi = true;
            gameMasterScript.allDecidedOnAi[myPlayerId] = true;
            amIAi = true;

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
    //private bool TimerReady()
    //{
    //    return timer <= 0.0f;
    //}
}
