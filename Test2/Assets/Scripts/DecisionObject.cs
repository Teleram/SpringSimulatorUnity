using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// an Object that stores die decision our ai makes,
// we can apply it
public class DecisionObject{

    // the name of the function we will call
    // setTarget() Xor setDestination()
    private string functionName;

    // the destination we will give to our NavMeshAgent, if we call setDestination()
    private Vector3 destination;

    // the target we will set, if we call setTarget()
    // can be null
    private GameObject target;

    public DecisionObject(string function, Vector3 dest, GameObject tar)
    {
        functionName = function;
        destination = dest;
        target = tar;
    }

    public DecisionObject(string function, Vector3 dest)
    {
        functionName = function;
        destination = dest;
    }

    public string getFunctionName()
    {
        return functionName;
    }

    public Vector3 getDestination()
    {
        return destination;
    }

    public GameObject getTarget()
    {
        return target;
    }
}
