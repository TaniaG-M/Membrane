using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MultipleChoiceController : MonoBehaviour
{
    public string questionId;
    public string transitionTo;
    public Button[] buttons;
    public void sendAnswer(string answer) {
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
        StartCoroutine(Post(answer));
    }

    public void forceFinish() {
        sendAnswer("");
    }

    IEnumerator Post(string answer) {
        string url = "https://membrane-dashboard.herokuapp.com/api/answers";
        var request = new UnityWebRequest(url, "POST");
        string serialized = serializedData(answer);
        Debug.Log("Data: " + serialized);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(serialized);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        Debug.Log("Status Code: " + request.responseCode);
        LevelLoader.Instance.startTransitionTo(transitionTo);
    }

    string serializedData(string answer) {
        string fullString = "{\"answer\":{";
        fullString += "\"player_uuid\": \"" + GlobalControl.Instance.localPlayer.uuid + "\", ";
        fullString += "\"seconds\": \"" + Time.timeSinceLevelLoad + "\", ";
        fullString += "\"question_reference\": \"" + questionId + "\", ";
        fullString += "\"text\": \"" + answer;
        return fullString += "\"}}";
    }
}
