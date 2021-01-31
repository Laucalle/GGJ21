using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeScroll : MonoBehaviour
{
    public int maxChildren = 4;

    private void OnTransformChildrenChanged()
    {
        //Debug.Log("A CHILD WA ADDED");
        if (transform.childCount > maxChildren)
        {
            Destroy(gameObject.transform.GetChild(0).gameObject);
        }
    }
}
