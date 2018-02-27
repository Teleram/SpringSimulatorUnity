using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TargetingScript : NetworkBehaviour {

    private SelectScript selectScript;

    private LivingScript livingScript;

    private GameMasterScript gameMasterScript;

    public GameObject target;

    public Vector3 myDestination;
    public bool destinationHasChanged;

    private bool amIAi;
    private AIBrain aiBrain;

	// Use this for initialization
	void Start ()
    {
        selectScript = GetComponent<SelectScript>();

        livingScript = GetComponent<LivingScript>();

        GameObject gameMaster = GameObject.Find("GameMaster");
        gameMasterScript = gameMaster.GetComponent<GameMasterScript>();
        aiBrain = gameMaster.GetComponent<AIBrain>();

        GameObject[] allPlayerObjects = GameObject.FindGameObjectsWithTag("PlayerObject");
        OnPlayerEnter myPlayerObject = null;
        foreach (GameObject playerObject in allPlayerObjects)
        {
            OnPlayerEnter playerEnterScript = playerObject.GetComponent<OnPlayerEnter>();
            if (playerEnterScript.myPlayerId == livingScript.myPlayerId)
            {
                myPlayerObject = playerEnterScript;
            }
        }

        amIAi = myPlayerObject.amIAi;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (hasAuthority && gameMasterScript.gameIsRunning())
        {
            if (amIAi)
            {
                GameObject[] dummyArray = new GameObject[0];
                DecisionObject myNextDecision = aiBrain.getDecisionForTargetscript(transform.position, dummyArray);
                switch(myNextDecision.getFunctionName())
                {
                    case "setDestination":
                        setDestination(myNextDecision.getDestination());
                        break;
                    case "setTarget":
                        setTarget(myNextDecision.getTarget());
                        break;
                    default :
                        Debug.Log("We messed up!");
                        break;
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(1) && selectScript.selected)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 1000))
                    {
                        if (hit.collider.tag == "PlayerControlled")
                        {
                            setTarget(hit.collider.gameObject);
                        }

                        if (hit.collider.tag == "Terrain")
                        {
                            setDestination(hit.point);
                        }
                    }
                }
            }

            if (hasTarget() && target.transform.position != myDestination)
            {
                myDestination = target.transform.position;
                destinationHasChanged = true;
            }
        }
    }

    // check if target is null
    public bool hasTarget()
    {
        if (target == null)
        {
            return false;
        }
        return true;
    }

    // if there is no target, the target is friendly
    public bool targetIsFriendly()
    {
        if (hasTarget())
        {
            LivingScript targetScript = target.GetComponent<LivingScript>();

            if (livingScript.myPlayerId == targetScript.myPlayerId)
            {
                return true;
            }

            return false;
        }

        return true;
    }

    public float distanceToTarget()
    {
        if(!hasTarget())
        {
            return 0.0f;
        }

        return Vector3.Distance(transform.position, target.transform.position);
    }

    private void setTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    private void setDestination(Vector3 destination)
    {
        myDestination = destination;
        destinationHasChanged = true;
        target = null;
    }
}
