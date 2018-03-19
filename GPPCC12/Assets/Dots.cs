using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dots : MonoBehaviour {

    [SerializeField]
    GameObject portalPrefab;

    [SerializeField]
    Vector2 portalPositon;

    public void CheckChildren()
    {
        if(transform.childCount == 1)
        {
            Instantiate(portalPrefab, portalPositon, Quaternion.identity);
        }
    }
}
