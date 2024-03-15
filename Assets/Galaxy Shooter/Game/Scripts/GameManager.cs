using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


    public bool gameOver = true;
    public GameObject playerPrefab;

    private UiManager _uiManager;
    private SpawnManager _spawnManager;

    void Start() {
        _uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    void Update() {
        if (gameOver == true) {
            if (Input.GetKeyDown(KeyCode.Return)) {
                Instantiate(playerPrefab, transform.position, Quaternion.identity);
                _uiManager.HideMenu();
                gameOver = false;
                _spawnManager.Respawn();
            }
        }
    }

    public void GameOver() {
        _uiManager.ShowMenu();
        _uiManager.ResetScore();

        gameOver = true;
    }
}
