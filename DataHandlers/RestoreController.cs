using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class RestoreController : MonoBehaviour
{
  public TMP_InputField emailText;
  public Button submitButton;
  public TMP_Text buttonText;
  
  public void saveUserDetails() {
    if(completeFields()) {
      submitButton.interactable = false;
      buttonText.text = "Enviando...";
      string randomId = Guid.NewGuid().ToString();
      GlobalControl.Instance.localPlayer.email = emailText.text;
      GlobalControl.Instance.localPlayer.uuid = randomId;
      Debug.Log("This happens");
      StartCoroutine(Post());
      GlobalControl.Instance.SaveData();
    }
  }

  bool completeFields() {
    if(emailText.text == "") {
      return false;
    }
    return true;
  }

  IEnumerator Post() {
    // string url = "https://membrane-dashboard.herokuapp.com/api/players/restores";
    string url = "https://membrane-dashboard.herokuapp.com/api/players/restores";
    var request = new UnityWebRequest(url, "POST");
    string serialized = serializedData();
    Debug.Log("Data: " + serialized);
    byte[] bodyRaw = Encoding.UTF8.GetBytes(serialized);
    request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
    request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
    request.SetRequestHeader("Content-Type", "application/json");
    yield return request.SendWebRequest();
    FullPlayerResponse loadedData = JsonUtility.FromJson<FullPlayerResponse>(request.downloadHandler.text);
    if(!String.IsNullOrEmpty(loadedData.uuid) && !String.IsNullOrEmpty(loadedData.email)) {
      saveSerializedData(loadedData);
      if(GlobalControl.Instance.localPlayer.answeredQuestion4) {
        LevelLoader.Instance.startTransitionTo("Experiment1");
      } else {
        LevelLoader.Instance.startTransitionTo("Pretest2");
      }
    } else {
      GlobalControl.Instance.clearData();
      LevelLoader.Instance.startTransitionTo("NotAuthorized");
    }
  }

  void saveSerializedData(FullPlayerResponse data) {
    GlobalControl.Instance.localPlayer.sessionCount = data.sessioncount++;
    GlobalControl.Instance.localPlayer.answeredQuestion4 = data.pretest;
    GlobalControl.Instance.localPlayer.answeredQuestion5 = data.posttest;
    GlobalControl.Instance.localPlayer.email = data.email;
    GlobalControl.Instance.localPlayer.uuid = data.uuid;
    GlobalControl.Instance.localPlayer.age = data.age;
    GlobalControl.Instance.localPlayer.school = data.school;
    GlobalControl.Instance.localPlayer.specialty = data.specialty;
    GlobalControl.Instance.SaveData();
  }

  string serializedData() {
    string fullString = "{\"player\":{";
    fullString += "\"uuid\": \"" + GlobalControl.Instance.localPlayer.uuid + "\", ";
    fullString += "\"email\": \"" + GlobalControl.Instance.localPlayer.email + "\"";
    return fullString += "}}";
  }
}
