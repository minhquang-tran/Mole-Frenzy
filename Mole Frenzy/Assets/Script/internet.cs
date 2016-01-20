using UnityEngine;

public class internet : MonoBehaviour
{
    private const bool allowCarrierDataNetwork = false;
    private const string pingAddress = "8.8.8.8"; // Google Public DNS server
    private const float waitingTime = 2.0f;
    public bool isInternetAvailable;

    private Ping ping;
    private float pingStartTime;

    public void Start()
    {
        isInternetAvailable = false;
       
    }

    public void Update()
    {
        if (ping != null)
        {
            bool stopCheck = true;
            if (ping.isDone)
                InternetAvailable();
            else if (Time.time - pingStartTime < waitingTime)
                stopCheck = false;
            else
                InternetIsNotAvailable();
            if (stopCheck)
                ping = null;
        }
    }

    private void InternetIsNotAvailable()
    {
        isInternetAvailable = false;
    }

    private void InternetAvailable()
    {
        isInternetAvailable = true;
    }

    public bool getConnection()
    {
        return isInternetAvailable;
    }

    public void checkConnection()
    {
        bool internetPossiblyAvailable;
        switch (Application.internetReachability)
        {
            case NetworkReachability.ReachableViaLocalAreaNetwork:
                internetPossiblyAvailable = true;
                break;
            case NetworkReachability.ReachableViaCarrierDataNetwork:
                internetPossiblyAvailable = allowCarrierDataNetwork;
                break;
            default:
                internetPossiblyAvailable = false;
                break;
        }
        if (!internetPossiblyAvailable)
        {
            InternetIsNotAvailable();
            return;
        }
        ping = new Ping(pingAddress);
        pingStartTime = Time.time;
    }
}