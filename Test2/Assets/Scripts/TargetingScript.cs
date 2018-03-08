using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using NeuralNetwork;

public class TargetingScript : NetworkBehaviour
{

    private SelectScript selectScript;
    private LivingScript livingScript;

    private GameMasterScript gameMasterScript;

    public GameObject target;

    public Vector3 myDestination;
    public bool destinationHasChanged;

    public float viewRange;

    private bool amIAi;
    private AIBrain aiBrain;

    private Vector3 referenceLocation;

    // Use this for initialization
    void Start()
    {
        selectScript = GetComponent<SelectScript>();
        livingScript = GetComponent<LivingScript>();

        GameObject gameMaster = GameObject.Find("GameMaster");
        gameMasterScript = gameMaster.GetComponent<GameMasterScript>();
        aiBrain = gameMaster.GetComponent<AIBrain>();

        referenceLocation = gameMasterScript.spawnpositions[livingScript.myPlayerId];

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
    void Update()
    {
        if (hasAuthority && gameMasterScript.gameIsRunning())
        {
            if (amIAi)
            {
                List<GameObject> objectsInViewRange = gameMasterScript.gameObjectsInRange(gameObject, viewRange);

                List<GameObject> friendsInViewRange = getSortedFriends(objectsInViewRange);
                List<GameObject> enemiesInViewRange = getSortedEnemies(objectsInViewRange);

                int myMainUnitIndex = findMainUnit(friendsInViewRange);
                int enemyMainUnitIndex = findMainUnit(enemiesInViewRange);

                DecisionObject myNextDecision = aiBrain.getDecisionForTargetscript(transform.position, referenceLocation, friendsInViewRange, enemiesInViewRange, myMainUnitIndex, enemyMainUnitIndex);
                switch (myNextDecision.getFunctionName())
                {
                    case "setDestination":
                        setDestination(myNextDecision.getDestination());
                        break;
                    case "setTarget":
                        setTarget(myNextDecision.getTarget());
                        break;
                    default:
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
        if (!hasTarget())
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

    private List<GameObject> getSortedFriends(List<GameObject> allObjects)
    {
        allObjects.Sort(new SortByDistance(transform.position));
        List<GameObject> sortedFriends = new List<GameObject>();
        foreach (GameObject go in allObjects)
        {
            int otherPlayerId = go.GetComponent<LivingScript>().myPlayerId;
            if (livingScript.myPlayerId == otherPlayerId)
            {
                sortedFriends.Add(go);
            }

        }
        return sortedFriends;
    }

    private List<GameObject> getSortedEnemies(List<GameObject> allObjects)
    {
        allObjects.Sort(new SortByDistance(transform.position));
        List<GameObject> sortedEnemies = new List<GameObject>();
        foreach (GameObject go in allObjects)
        {
            int otherPlayerId = go.GetComponent<LivingScript>().myPlayerId;
            if (livingScript.myPlayerId != otherPlayerId)
            {
                sortedEnemies.Add(go);
            }

        }
        return sortedEnemies;
    }

    private int findMainUnit(List<GameObject> units)
    {
        int i;
        for (i = 0; i < units.Count; i++)
        {
            GameObject currentUnit = (GameObject)units[i];
            if (currentUnit.GetComponent<Spawn>() != null)
            {
                return i;
            }
        }
        return -1;
    }
}
