using UnityEngine;

[System.Serializable]
public class AnswerResponse
{
  public int id;
  public int question_id;
  public string text;
  public string created_at;
  public string updated_at;
  public bool unlock;
}