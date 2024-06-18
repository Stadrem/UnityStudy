using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosFunction : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one * 3);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(6, 0, 0), 1.5f);
    }
}
