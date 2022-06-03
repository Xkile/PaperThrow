using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    MeshCollider dustbin_col1;
    [SerializeField]
    MeshCollider dustbin_col2;
    [SerializeField]
    Collider dustbin_col3;

    private void OnTriggerEnter(Collider other)
    {
        //dustbin_col1.convex = false;
        //dustbin_col2.convex = false;
        dustbin_col3.enabled = false;
    }
}
