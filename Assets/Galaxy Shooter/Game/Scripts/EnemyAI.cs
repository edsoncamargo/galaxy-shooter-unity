using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    private float _speed = 5.0f;

    private float initialVerticalArea = 6f;
    private float maxVerticalArea = -6f;
    private float minHorizontalArea = -8.0f;
    private float maxHorizontalArea = 8.0f;

    [SerializeField]
    private GameObject _explosionPrefab;

    private UiManager _uiManager;

    [SerializeField]
    private AudioClip _clip;

    void Start() {
        Respawn();

        if (_uiManager != null) {
            _uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        }
    }

    void Update() {
        HandleVerticalBounds();
        HandleMoveDown();
    }

    private void HandleVerticalBounds() {
        if (transform.position.y < maxVerticalArea) {
            Respawn();
            return;
        }
    }

    private void HandleMoveDown() {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
    }

    private void Respawn() {
        float randomHorizontalArea = Random.Range(minHorizontalArea, maxHorizontalArea);
        transform.position = new Vector3(randomHorizontalArea, initialVerticalArea, 0);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        HandleCollided(other);
    }


    public void HandleCollided(Collider2D other) {
        switch (other.tag) {
            case "Laser":
            GenerateExplosion();
            Destroy(gameObject);
            break;
            case "Triple_Shoot_PowerUp":
            GenerateExplosion();
            Destroy(gameObject);
            break;
            case "Player":
            Player player = other.GetComponent<Player>();
            if (player == null)
                return;

            player.HandleCollidedWithTheEnemyShip();
            GenerateExplosion();
            Destroy(gameObject);
            break;
        }
    }

    private void GenerateExplosion() {
        AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);

        if (_uiManager != null) {
            _uiManager.UpdateScore(10);
        }

        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
    }
}
