using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class SessionManager : MonoBehaviour
{
    public bool increaseSessionCount = false;
    public string sceneName = "";
    private void Start() {
        if(GlobalControl.Instance.localPlayer.uuid != "") {
            if(increaseSessionCount) {
                GlobalControl.Instance.localPlayer.sessionCount++;
                GlobalControl.Instance.SaveData();
            }
            StartCoroutine(Post());
        }
    }
    IEnumerator Post() {
        string url = "https://membrane-dashboard.herokuapp.com/api/scene_times";
        var request = new UnityWebRequest(url, "POST");
        string serialized = serializedData();
        Debug.Log("Data: " + serialized);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(serialized);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        Debug.Log("Status Code: " + request.responseCode);
    }

    string serializedData() {
        string fullString = "{\"scene_time\":{";
        fullString += "\"player_uuid\": \"" + GlobalControl.Instance.localPlayer.uuid + "\", ";
        fullString += "\"seconds\": \"" + Time.timeSinceLevelLoad + "\", ";
        fullString += "\"scene_name\": \"" + sceneName + "\", ";
        fullString += "\"count\": \"" + GlobalControl.Instance.localPlayer.sessionCount;
        return fullString += "\"}}";
    }
}
