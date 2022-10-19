using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class InputFormController : MonoBehaviour
{
  public TMP_InputField nameText;
  public TMP_InputField emailText;
  public TMP_InputField ageText;
  public TMP_InputField specialtyText;
  public TMP_InputField schoolText;
  public Button submitButton;
  public TMP_Text buttonText;
  
  public void saveUserDetails() {
    if(completeFields()) {
      submitButton.interactable = false;
      buttonText.text = "Enviando...";
      string randomId = Guid.NewGuid().ToString();
      GlobalControl.Instance.localPlayer.name = nameText.text;
      GlobalControl.Instance.localPlayer.email = emailText.text;
      GlobalControl.Instance.localPlayer.age = ageText.text;
      GlobalControl.Instance.localPlayer.school = schoolText.text;
      GlobalControl.Instance.localPlayer.specialty = specialtyText.text;
      GlobalControl.Instance.localPlayer.uuid = randomId;
      StartCoroutine(Post());
      GlobalControl.Instance.SaveData();
    }
  }

  bool completeFields() {
    if(nameText.text == "" || emailText.text == "" || ageText.text == "" || schoolText.text == "") {
      return false;
    }
    return true;
  }

  IEnumerator Post() {
    string url = "https://membrane-dashboard.herokuapp.com/api/players";
    // string url = "https://membrane-dashboard.herokuapp.com/api/players";
    var request = new UnityWebRequest(url, "POST");
    string serialized = serializedData();
    Debug.Log("Data: " + serialized);
    byte[] bodyRaw = Encoding.UTF8.GetBytes(serialized);
    request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
    request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
    request.SetRequestHeader("Content-Type", "application/json");
    yield return request.SendWebRequest();
    PlayerResponse loadedData = JsonUtility.FromJson<PlayerResponse>(request.downloadHandler.text);
    if(loadedData.valid_player) {
      if(loadedData.complete_pre_test) {
        LevelLoader.Instance.startTransitionTo("Experiment1");
      } else {
        GlobalControl.Instance.localPlayer.sessionCount++;
        GlobalControl.Instance.SaveData();
        StartCoroutine(PostSession());
      }
    } else {
      GlobalControl.Instance.clearData();
      LevelLoader.Instance.startTransitionTo("NotAuthorized");
    }
  }

  string serializedData() {
    string fullString = "{\"player\":{";
    fullString += "\"uuid\": \"" + GlobalControl.Instance.localPlayer.uuid + "\", ";
    fullString += "\"email\": \"" + GlobalControl.Instance.localPlayer.email + "\", ";
    fullString += "\"name\": \"" + GlobalControl.Instance.localPlayer.name + "\", ";
    fullString += "\"age\": \"" + GlobalControl.Instance.localPlayer.age + "\", ";
    fullString += "\"school\": \"" + GlobalControl.Instance.localPlayer.school + "\", ";
    fullString += "\"specialty\": \"" + GlobalControl.Instance.localPlayer.specialty + "\"";
    return fullString += "}}";
  }

  IEnumerator PostSession() {
    string url = "https://membrane-dashboard.herokuapp.com/api/scene_times";
    // string url = "https://membrane-dashboard.herokuapp.com/api/scene_times";
    var request = new UnityWebRequest(url, "POST");
    string serialized = sessionSerializedData();
    Debug.Log("Data: " + serialized);
    byte[] bodyRaw = Encoding.UTF8.GetBytes(serialized);
    request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
    request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
    request.SetRequestHeader("Content-Type", "application/json");
    yield return request.SendWebRequest();
    Debug.Log("Status Code: " + request.responseCode);
    LevelLoader.Instance.startTransitionTo("Pretest2");
  }

  string sessionSerializedData() {
    string fullString = "{\"scene_time\":{";
    fullString += "\"player_uuid\": \"" + GlobalControl.Instance.localPlayer.uuid + "\", ";
    fullString += "\"seconds\": \"" + Time.timeSinceLevelLoad + "\", ";
    fullString += "\"scene_name\": \"" + "userInput" + "\", ";
    fullString += "\"count\": \"" + GlobalControl.Instance.localPlayer.sessionCount;
    return fullString += "\"}}";
  }
}
