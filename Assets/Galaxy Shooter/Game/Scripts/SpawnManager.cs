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

    void Start() {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Respawn();
    }

    public void Respawn() {
        StartCoroutine(GenerateEnemies());
        StartCoroutine(GeneratePowerUps());
    }

    IEnumerator GenerateEnemies() {
        while (_gameManager.gameOver == false) {
            Instantiate(enemyShipPrefab);
            yield return new WaitForSeconds(2.0f);
        }
    }

    IEnumerator GeneratePowerUps() {
        while (_gameManager.gameOver == false) {
            int powerUpRandomIndex = _lastPowerUpRespawnedIndex;
            do {
                powerUpRandomIndex = Random.Range(0, powerUpsPrefab.Length);
            }
            while (powerUpRandomIndex == _lastPowerUpRespawnedIndex);

            _lastPowerUpRespawnedIndex = powerUpRandomIndex;
            Instantiate(powerUpsPrefab[powerUpRandomIndex]);
            yield return new WaitForSeconds(8.0f);
        }
    }
}
