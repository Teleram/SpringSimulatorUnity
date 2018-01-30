using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public int hp;
    public GameObject me;
    public float counter;
    public GameObject unittype;
    public float spawnspeed;
    public Vector3 spawnpos;
    //public Transform[] spawnPoints;


	// Use this for initialization
	void Start () {
        hp = 1000;
	}
	
	// Update is called once per frame
	void Update () {
        //float hp_temp = hp - (10 * Time.deltaTime);
        //hp = (int) hp_temp;

        if(hp <= 0)
        {
            //me.active = false;
            Destroy(me);
        }

        if(counter >= 100 && spawnposisfree(spawnpos))
        {
            //Vector3 pos = new Vector3(100, 100, 100);
            Quaternion rot = new Quaternion();
            Instantiate(unittype, spawnpos, rot);
            counter -= 100;
        }

        counter += spawnspeed * Time.deltaTime;
	}

    private bool spawnposisfree(Vector3 spawnposLocal)
    {
        return true;
    }
}
