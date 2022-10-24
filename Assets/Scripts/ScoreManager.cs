using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;

public class ScoreManager : MonoBehaviour
{
    private TMP_Text scoreLabel;
    private int score = 0;

    [SerializeField]
    private Slider slider;

    [SerializeField]
    float recoverSpeed;

    Color sliderColor;
    // Start is called before the first frame update
    void Start()
    {
      //  slider = sliderObj.GetComponent<Slider>();
        scoreLabel = GameObject.Find("ScoreLabel").GetComponent<TMP_Text>();

        sliderColor = slider.fillRect.GetComponent<Image>().color;

        UpdateScoreLabel();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        slider.value += recoverSpeed;

        if (slider.value < .4)
            slider.fillRect.GetComponent<Image>().color = Color.red;
        else if (slider.value >= .3 && slider.fillRect.GetComponent<Image>().color == Color.red)
            slider.fillRect.GetComponent<Image>().color = sliderColor;


    }

    public int GetScore()
    {
        return score;       
    }

    public void SetScore(int  newScore)
    {
        score = newScore;
        UpdateScoreLabel();
    }

    public void AddPoint(int point)
    {
        score += point;
        UpdateScoreLabel();
    }

    public void UpdateScoreLabel()
    {
        scoreLabel.text = $"Score: {score}";
    }

    public void getHit(int hitAmount)
    {
        Debug.Log("getHit called");
        float value = hitAmount * 0.01f;
        slider.value -= value;

        if (slider.value <= 0)
        {
           
            FindObjectOfType<Player>().StartCoroutine("ExplosionCoroutine");
            this.enabled = false;
        }
    }
}
