﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class UnitBehavior : NetworkBehaviour {

    public GameObject me;
    public int hp = 100;

    [SyncVar]
    public int myPlayerId;

	// Use this for initialization
	void Start () 
    {
        GameObject gameMaster = GameObject.Find("GameMaster");
        GameMasterScript gameMasterScript = (GameMasterScript)gameMaster.GetComponent("GameMasterScript");
        Material myMaterial = gameMasterScript.materials[myPlayerId];

        Renderer renderer = me.GetComponent<Renderer>();
        renderer.material = myMaterial;
    }
	
	// Update is called once per frame
	void Update () {
        if (!hasAuthority)
            return;
        if (me.transform.position[1] <= -100 || hp <= 0)
        {
           // Destroy(pointer);
            Destroy(me);
        }
        
	}
}