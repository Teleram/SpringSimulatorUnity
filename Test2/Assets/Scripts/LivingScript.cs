using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LivingScript : NetworkBehaviour {

    [SyncVar]
    public int hp;

    [SyncVar]
    public int myPlayerId;


    // public for debug
    public int myObjectId;

    private GameMasterScript gameMasterScript;

    void Start()
    {
        GameObject gameMaster = GameObject.Find("GameMaster");
        gameMasterScript = gameMaster.GetComponent<GameMasterScript>();
        Material myMaterial = gameMasterScript.materials[myPlayerId];

        Renderer renderer = gameObject.GetComponent<Renderer>();
        renderer.material = myMaterial;

        //assignMyObjectId();
        myObjectId = -1;
    }

    void Update()
    {
        if (myObjectId == -1)
        {
            assignMyObjectId();
        }
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void assignMyObjectId()
    {
        myObjectId = gameMasterScript.getIdForObject(gameObject);
    }

    public void takeDamage(int damage)
    {
        if(hasAuthority)
        {
            CmdTakeDamage(damage);
        }
    }

    [Command]
    private void CmdTakeDamage(int damage)
    {
        hp -= damage;
    }
}
