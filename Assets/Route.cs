using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    Transform[] childObjects;
    public List<Transform> childNodeList = new List<Transform>();

    void Start()
    {
        FillNodes();
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        FillNodes();

        for (int i = 0; i < childNodeList.Count; i++)
        {
            Vector3 currentPos = childNodeList[i].position;
            if (i > 0)
            {
                Vector3 prefPos = childNodeList[i - 1].position;
                Gizmos.DrawLine(prefPos, currentPos);
            }
        }
    }

    void FillNodes()
    {
        childNodeList.Clear();

        childObjects = GetComponentsInChildren<Transform>();

        foreach(Transform child in childObjects)
        {
            if(child != this.transform)
            {
                childNodeList.Add(child);
            }
        }
    }

}
