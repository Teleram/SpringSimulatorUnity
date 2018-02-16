using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameMasterScript : NetworkBehaviour {

    public Vector3[] spawnpositions;

    public Material[] materials;

    //[SyncVar]
    private int idForNextPlayer;

	// Use this for initialization
	void Start () {
        idForNextPlayer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int getIdForNewPlayer()
    {
        return idForNextPlayer++;
    }
}
