using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{


    public Sprite[] lives;
    public Image livesImageDisplay;
    public GameObject menuDisplay;
    private int _score = 0;

    public Text scoreDisplay;

    public void UpdateLives(int currentLives) {
        livesImageDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore(int shipScore) {
        _score = _score + shipScore;
        scoreDisplay.text = "Score: " + _score.ToString();
    }

    public void ResetScore() {
        scoreDisplay.text = "Score: " + 0.ToString();
    }

    public void ShowMenu() {
        menuDisplay.SetActive(true);
    }

    public void HideMenu() {
        menuDisplay.SetActive(value: false);
    }
}
