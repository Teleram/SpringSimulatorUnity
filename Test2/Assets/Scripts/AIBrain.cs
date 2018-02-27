using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour {

	public DecisionObject getDecisionForTargetscript(Vector3 position, GameObject[] targetsInRange)
    {
        Vector3 newPosition;
        newPosition.x = (position.x + (12 * Time.deltaTime)) % 500;
        newPosition.y = 0;
        newPosition.z = (position.z + (10 * Time.deltaTime)) % 500;

        return new DecisionObject("setDestination", newPosition);
    }
}
