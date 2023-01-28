using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public NetworkConnectivity conn;

    void Start()
    {
        conn.StartConnectivityAlerts(OnStatusChanged);
        

        // Stop connectivity alerts
        //conn.StopConnectivityAlerts();

        // Check connectivity at any time after starting alerts
        Debug.Log("Connected = " + conn.Connected);
    }

    void OnStatusChanged(bool connected)
    {
        Debug.Log("Network status changed. Connected = " + connected);
    }
}
