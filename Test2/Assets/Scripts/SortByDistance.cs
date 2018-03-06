using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortByDistance : IComparer<GameObject> {

    private readonly Vector3 myPosition;

    public SortByDistance(Vector3 pos)
    {
        myPosition = pos;
    }

	public int Compare(GameObject go1, GameObject go2)
    {
        float dist1 = Vector3.Distance(myPosition, go1.transform.position);
        float dist2 = Vector3.Distance(myPosition, go2.transform.position);

        if(dist1 >= dist2)
        {
            return 1;
        }

        if(dist1 == dist2)
        {
            return 0;
        }

        return -1;
    }
}
