using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LivingScript : NetworkBehaviour {

    public GameObject me;

    [SyncVar]
    public int hp;

    [SyncVar]
    public int myPlayerId;

    void Start()
    {
        GameObject gameMaster = GameObject.Find("GameMaster");
        GameMasterScript gameMasterScript = (GameMasterScript)gameMaster.GetComponent("GameMasterScript");
        Material myMaterial = gameMasterScript.materials[myPlayerId];

        Renderer renderer = me.GetComponentInChildren<Renderer>();
        renderer.material = myMaterial;
    }

    void Update()
    {
        if(hp <= 0)
        {
            Destroy(me);
        }
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
