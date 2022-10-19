using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
  public static GlobalControl Instance { get; private set; }
  public PlayerStats localPlayer;

  void Awake ()
  {
    if (Instance == null)
    {
      DontDestroyOnLoad(gameObject);
      Instance = this;
      if(localPlayer == null) localPlayer = new PlayerStats();
      LoadData();
    }
    else if (Instance != this)
    {
      Destroy(gameObject);
    }
  }

  public void SaveData()
  {
    PlayerPrefs.SetString("Uuid", localPlayer.uuid);
    PlayerPrefs.SetString("Name", localPlayer.name);
    PlayerPrefs.SetString("Email", localPlayer.email);
    PlayerPrefs.SetString("Age", localPlayer.age);
    PlayerPrefs.SetString("School", localPlayer.school);
    PlayerPrefs.SetString("Specialty", localPlayer.specialty);
    PlayerPrefs.SetInt("Welcomed", localPlayer.welcomed ? 1 : 0);
    PlayerPrefs.SetInt("SessionCount", localPlayer.sessionCount);
    PlayerPrefs.SetInt("AnsweredQuestion1", localPlayer.answeredQuestion1 ? 1 : 0);
    PlayerPrefs.SetInt("AnsweredQuestion2", localPlayer.answeredQuestion2 ? 1 : 0);
    PlayerPrefs.SetInt("AnsweredQuestion3", localPlayer.answeredQuestion3 ? 1 : 0);
    PlayerPrefs.SetInt("AnsweredQuestion4", localPlayer.answeredQuestion4 ? 1 : 0);
    PlayerPrefs.SetInt("AnsweredQuestion5", localPlayer.answeredQuestion4 ? 1 : 0);
  }

  public void LoadData()
  {
    localPlayer.uuid = PlayerPrefs.GetString("Uuid", "");
    localPlayer.name = PlayerPrefs.GetString("Name", "");
    localPlayer.email = PlayerPrefs.GetString("Email", "");
    localPlayer.email = PlayerPrefs.GetString("Age", "");
    localPlayer.email = PlayerPrefs.GetString("School", "");
    localPlayer.email = PlayerPrefs.GetString("Specialty", "");
    localPlayer.welcomed = PlayerPrefs.GetInt("Welcomed", 0) != 0;
    localPlayer.sessionCount = PlayerPrefs.GetInt("SessionCount", 0);
    localPlayer.answeredQuestion1 = PlayerPrefs.GetInt("AnsweredQuestion1", 0) != 0;
    localPlayer.answeredQuestion2 = PlayerPrefs.GetInt("AnsweredQuestion2", 0) != 0;
    localPlayer.answeredQuestion3 = PlayerPrefs.GetInt("AnsweredQuestion3", 0) != 0;
    localPlayer.answeredQuestion4 = PlayerPrefs.GetInt("AnsweredQuestion4", 0) != 0;
    localPlayer.answeredQuestion4 = PlayerPrefs.GetInt("AnsweredQuestion5", 0) != 0;
  }

  public void clearData() {
    PlayerPrefs.SetString("Uuid", "");
    PlayerPrefs.SetString("Name", "");
    PlayerPrefs.SetString("Email", "");
    PlayerPrefs.SetString("Age", "");
    PlayerPrefs.SetString("School", "");
    PlayerPrefs.SetString("Specialty", "");
    PlayerPrefs.SetInt("Welcomed", 0);
    PlayerPrefs.SetInt("SessionCount", 0);
    PlayerPrefs.SetInt("AnsweredQuestion1", 0);
    PlayerPrefs.SetInt("AnsweredQuestion2", 0);
    PlayerPrefs.SetInt("AnsweredQuestion3", 0);
    PlayerPrefs.SetInt("AnsweredQuestion4", 0);
    PlayerPrefs.SetInt("AnsweredQuestion5", 0);
  }
}
