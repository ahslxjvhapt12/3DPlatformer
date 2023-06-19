using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    [SerializeField] GameObject respawnPos;
    [SerializeField] Vector3 boxSize;
    private void Update()
    {
        Collider[] cols = Physics.OverlapBox(transform.position, boxSize * 0.5f);
        if (cols.Length != 0)
        {
            cols.ToList().ForEach(col => { col.transform.position = respawnPos.transform.position; });
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, boxSize);
    }
}
