using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LevelLoader : MonoBehaviour
{
  [SerializeField, Tooltip("Transition container.")]
  Animator transition = null;
  public static LevelLoader Instance { get; private set; }
  bool started = false;
  string nextSceneName = "";

  void Awake() {
    if (Instance == null) Instance = this;
  }

  void Update()
  {
    if(started) {
      started = false;
      StartCoroutine(LoadLevel());
    }
  }

  public void startTransitionTo(string sceneName) {
    nextSceneName = sceneName;
    started = true;
  }

  IEnumerator LoadLevel() {
    transition.SetTrigger("Fade");
    yield return new WaitForSeconds(1f);
    SceneManager.LoadScene(nextSceneName);
  }
}
