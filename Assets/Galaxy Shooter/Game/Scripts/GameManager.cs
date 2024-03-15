using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


    public bool gameOver = true;
    public GameObject playerPrefab;

    private UiManager _uiManager;

    void Start() {
        _uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
    }

    void Update() {
        if (gameOver == true) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                Instantiate(playerPrefab, transform.position, Quaternion.identity);
                _uiManager.HideMenu();
                gameOver = false;
            }
        }
    }

    public void GameOver() {
        _uiManager.ShowMenu();
        _uiManager.ResetScore();

        gameOver = true;
    }
}
