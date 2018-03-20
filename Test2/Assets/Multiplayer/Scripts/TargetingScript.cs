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
        if (hasAuthority && gameMasterScript.GameIsRunning())
        {
            if (amIAi)
            {
                List<GameObject> objectsInViewRange = gameMasterScript.GameObjectsInRange(gameObject, viewRange);

                List<GameObject> friendsInViewRange = GetSortedFriends(objectsInViewRange);
                List<GameObject> enemiesInViewRange = GetSortedEnemies(objectsInViewRange);

                int myMainUnitIndex = FindMainUnit(friendsInViewRange);
                int enemyMainUnitIndex = FindMainUnit(enemiesInViewRange);

                DecisionObject myNextDecision = aiBrain.GetDecisionForTargetscript(transform.position, referenceLocation, friendsInViewRange, enemiesInViewRange, myMainUnitIndex, enemyMainUnitIndex);
                switch (myNextDecision.getFunctionName())
                {
                    case "setDestination":
                        SetDestination(myNextDecision.getDestination());
                        break;
                    case "setTarget":
                        SetTarget(myNextDecision.getTarget());
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
                            SetTarget(hit.collider.gameObject);
                        }

                        if (hit.collider.tag == "Terrain")
                        {
                            SetDestination(hit.point);
                        }
                    }
                }
            }

            if (HasTarget() && target.transform.position != myDestination)
            {
                myDestination = target.transform.position;
                destinationHasChanged = true;
            }
        }
    }

    // check if target is null
    public bool HasTarget()
    {
        if (target == null)
        {
            return false;
        }
        return true;
    }

    // if there is no target, the target is friendly
    public bool TargetIsFriendly()
    {
        if (HasTarget())
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

    public float DistanceToTarget()
    {
        if (!HasTarget())
        {
            return 0.0f;
        }

        return Vector3.Distance(transform.position, target.transform.position);
    }

    private void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    private void SetDestination(Vector3 destination)
    {
        myDestination = destination;
        destinationHasChanged = true;
        target = null;
    }

    private List<GameObject> GetSortedFriends(List<GameObject> allObjects)
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

    private List<GameObject> GetSortedEnemies(List<GameObject> allObjects)
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

    private int FindMainUnit(List<GameObject> units)
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
