using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecommissionFunction : MonoBehaviour
{
    private void OnDestroy()
    {
        Debug.Log("OnDestroy!");
    }

    private void OnApplicationQuit()
    {
        Debug.Log("OnapplicationQuit!");
    }

    private void OnDisable()
    {
        Debug.Log("Ondisable!");
    }
}
