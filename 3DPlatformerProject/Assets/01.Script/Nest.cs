using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour
{

    private void Update()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 1, 1 << 6);
        if (cols.Length != 0)
        {
            Debug.Log("»õ µµÂø");
        }
    }

    private void OnDrawGizmos()
    {

    }
}
