using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class SimpleAnswerController : MonoBehaviour
{
  public TMP_InputField[] multipleAnswers = {};
  public TMP_InputField answerText;
  public Button submitButton;
  public string questionReference;
  public string nextScene;

  public void sendAnswerToQuestion() {
    submitButton.interactable = false;
    StartCoroutine(Post());
  }

  public void forceFinish() {
    sendAnswerToQuestion();
  }
  
  IEnumerator Post() {
    string url = "https://membrane-dashboard.herokuapp.com/api/answers";
    var request = new UnityWebRequest(url, "POST");
    string serialized = serializedData();
    Debug.Log("Data: " + serialized);
    byte[] bodyRaw = Encoding.UTF8.GetBytes(serialized);
    request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
    request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
    request.SetRequestHeader("Content-Type", "application/json");
    yield return request.SendWebRequest();
    Debug.Log("Status Code: " + request.responseCode);
    Debug.Log("Status Code: " + request.result);
    PlayerResponse loadedData = JsonUtility.FromJson<PlayerResponse>(request.downloadHandler.text);
    if(nextScene == "Experiment1") {
      if(SceneManager.GetActiveScene().name == "Posttest5") {
        GlobalControl.Instance.localPlayer.answeredQuestion5 = true;
      } else {
        GlobalControl.Instance.localPlayer.answeredQuestion4 = true;
      }
      GlobalControl.Instance.SaveData();
    }
    LevelLoader.Instance.startTransitionTo(nextScene);
  }

  string serializedData() {
    string answer = "";
    if(multipleAnswers.Length > 0) {
      foreach (TMP_InputField answerField in multipleAnswers)
      {
        answer += answerField.text + ", ";
      }
      answer = answer.Remove(answer.Length - 2, 1);
    } else {
      answer = answerText.text;
    }
    string fullString = "{\"answer\":{";
    fullString += "\"player_uuid\": \"" + GlobalControl.Instance.localPlayer.uuid + "\", ";
    fullString += "\"seconds\": \"" + Time.timeSinceLevelLoad + "\", ";
    fullString += "\"question_reference\": \"" + questionReference + "\", ";
    fullString += "\"text\": \"" + answer;
    return fullString += "\"}}";
  }
}
