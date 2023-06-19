using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailBox : MonoBehaviour
{
    private void Update()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position + new Vector3(0, 1, 0), 0.5f, 1 << 6);
        if (cols.Length != 0)
        {
            Debug.Log("»õ µµÂø");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, 1, 0), 0.5f);
    }
}
