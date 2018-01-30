using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitBehavior : MonoBehaviour {

    public GameObject me;
    public int hp = 100;

    // public GameObject type;
    // private GameObject pointer;

	// Use this for initialization
	void Start () 
    {
        // Vector3 pos = new Vector3(100, 1, 100);
        // Quaternion rot = new Quaternion();
        // pointer = Instantiate(type, pos, rot);
    }
	
	// Update is called once per frame
	void Update () {
        if (me.transform.position[1] <= -100 || hp <= 0)
        {
           // Destroy(pointer);
            Destroy(me);
        }
        
	}
}
