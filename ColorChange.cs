using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private float speed = 10;
    public Color colorStart;
    public Color colorEnd;
    public SpriteRenderer square;
    public Animator liquidAnimator;
    public Animator lineAnimator;
    public string lineAnimationName;

    private void Awake() {
        
    }

    void Start()
    {
        square.color = colorStart;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startPour() {
        liquidAnimator.Play("vase_empty", 0 , 0);
        StartCoroutine(changeBgColor(0));
        lineAnimator.Play(lineAnimationName, 0 , 0);
    }

    private IEnumerator changeBgColor(float time)
    {
        yield return new WaitForSeconds(time);
        float tick = 0f;
        while (square.color != colorEnd)
        {
            tick += Time.deltaTime * speed;
            square.color = Color.Lerp(colorStart, colorEnd, tick);
            yield return null;
        }
    }
}
