using System;
using System.Collections;
using UnityEngine;
using System.Net.NetworkInformation;
using Ping = System.Net.NetworkInformation.Ping;

public class NetworkConnectivity : MonoBehaviour
{
    private static readonly string PingIP = "8.8.8.8";
    private static readonly float CheckIntervalWhenConnected = 15.0f;
    private static readonly float CheckIntervalWhenDisconnected = 4.0f;

    private delegate void OnStatusChanged(bool connected);
    private event OnStatusChanged onStatusChanged;
    private bool connectionStatus = false;
    private bool running = false;

    public bool Connected { get { return connectionStatus; } }

    public void StartConnectivityAlerts(Action<bool> onStatusChanged)
    {
        this.onStatusChanged += new OnStatusChanged(onStatusChanged);

        if (!running)
        {
            running = true;
            StartCoroutine(CheckNetworkConnectivity());
        }
    }

    public void StopConnectivityAlerts()
    {
        StopAllCoroutines();
        onStatusChanged = null;
        running = false;
    }

    IEnumerator CheckNetworkConnectivity()
    {
        while (true)
        {
            yield return null;

            bool _connected = false;
            try
            {
                Ping ping = new Ping();
                PingReply reply = ping.Send(PingIP);
                _connected = reply?.Status == IPStatus.Success;
            }
            catch { }

            if (connectionStatus != _connected)
            {
                connectionStatus = _connected;
                onStatusChanged?.Invoke(_connected);
            }
            yield return new WaitForSeconds
                (_connected ? CheckIntervalWhenConnected : CheckIntervalWhenDisconnected);
        }
    }
}
