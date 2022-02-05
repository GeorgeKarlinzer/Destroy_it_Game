using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Server : MonoBehaviour
{
    void Start()
    {
        //StartCoroutine(SendGetRequest());
    }

    IEnumerator SendGetRequest()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://localhost/MyStuff/getphp.php");

        yield return request.SendWebRequest();

        Debug.Log(request.downloadHandler.text);
    }

    IEnumerator SendPostRequest()
    {
        yield return null;
    }
}
