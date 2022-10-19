using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
  void Start()
  {
    GlobalControl.Instance.LoadData();
    Debug.Log("uuid: " + GlobalControl.Instance.localPlayer.uuid);
  }

  public void ResetPlayerPreferences() {
    PlayerPrefs.DeleteAll();
  }

  public void startTransitionTo(string sceneName) {
    GlobalControl.Instance.SaveData();
    LevelLoader.Instance.startTransitionTo(sceneName);
  }

  IEnumerator GetAnswer() {
    string baseUrl = "https://pokecinves.herokuapp.com/api/answers";
    string query = "player_uuid=" + GlobalControl.Instance.localPlayer.uuid + "&question_reference=pregunta-1";
    string url = baseUrl + "?" + query;
    var request = UnityWebRequest.Get(url);
    yield return request.SendWebRequest();
    if (request.result != UnityWebRequest.Result.Success) {
      Debug.Log(request.error);
    }
  }
}
