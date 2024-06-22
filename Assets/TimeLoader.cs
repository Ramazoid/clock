using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Unity.Loading;
using UnityEngine;
using UnityEngine.Networking;

public class TimeLoader : MonoBehaviour
{
    private static TimeLoader IN;
    private static string ResultString;
    private bool indicator;
    private string StoredURL;
    private Action<string> StoredCallback;
    private static Action<string> callback;
    private string resultString;
    public GameObject loadingIndicator;

    private void Awake()
    {
        IN = this;
    }
   
    IEnumerator callRequest(string url)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(url);
        yield return uwr.SendWebRequest();
        if (uwr.result==UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }
        Response info = JsonUtility.FromJson<Response>(uwr.downloadHandler.text);
        if(callback!=null)
            callback(info.time);  
    }

    internal void GetTime(string url, Action<string> _callback)
    {
        indicator = true;
        StoredURL = url;
        StoredCallback = callback = _callback;
        StartCoroutine(callRequest(url));
        print("**start delay");
        StartCoroutine(DelayAndHideAlert());
    }
    IEnumerator DelayAndHideAlert()
    {
        yield return new WaitForSeconds(3);
        indicator = false;
    }
    private void Update()
    {
        if (indicator)
            loadingIndicator.SetActive(!loadingIndicator.activeSelf);
        else
            loadingIndicator.SetActive(false);

    }

    internal static void GetTime()
    {
        IN.GetTime(IN.StoredURL, IN.StoredCallback);
    }
}
public struct Response
{
    public string time;
    public object clocks;
}
