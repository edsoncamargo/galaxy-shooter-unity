using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [SerializeField]
    private GameObject enemyShipPrefab;
    [SerializeField]
    private GameObject[] powerUpsPrefab;

    private int _lastPowerUpRespawnedIndex = -1;

    private GameManager _gameManager;
    private bool _isGameOver = true;
    private bool _isStart = false;

    void Start() {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Debug.Log("ENTREI");
    }

    void Update() {
        if (_gameManager != null) {
            _isGameOver = _gameManager.gameOver;

            if (_isGameOver != true && _isStart == false) {
                _isStart = true;
                StartCoroutine(GenerateEnemies());
                StartCoroutine(GeneratePowerUps());
            }
        }
    }

    IEnumerator GenerateEnemies() {
        if (_isGameOver != true) {
            Instantiate(enemyShipPrefab);
            yield return new WaitForSeconds(2.0f);
            StartCoroutine(GenerateEnemies());
        }
    }

    IEnumerator GeneratePowerUps() {
        if (_isGameOver != true) {
            int powerUpRandomIndex = _lastPowerUpRespawnedIndex;
            do {
                powerUpRandomIndex = Random.Range(0, powerUpsPrefab.Length);
            }
            while (powerUpRandomIndex == _lastPowerUpRespawnedIndex);

            _lastPowerUpRespawnedIndex = powerUpRandomIndex;
            Instantiate(powerUpsPrefab[powerUpRandomIndex]);
            yield return new WaitForSeconds(8.0f);
            StartCoroutine(GeneratePowerUps());
        }
    }
}
