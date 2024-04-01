using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
    [SerializeField] private float speed = 2.0f;

    private float initialVerticalArea = 6f;
    private float minHorizontalArea = -8.0f;
    private float maxHorizontalArea = 8.0f;

    [SerializeField] private AudioClip _clip;

    private void Start() {
        Respawn();
    }

    private void Respawn() {
        float randomHorizontalArea = Random.Range(minHorizontalArea, maxHorizontalArea);
        transform.position = new Vector3(randomHorizontalArea, initialVerticalArea, 0);
    }

    private void Update() {
        MovePowerUp();
        CheckDestroy();
    }

    private void MovePowerUp() {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }

    private void CheckDestroy() {
        if (transform.position.y < -6f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Player player = other.GetComponent<Player>();

            if (player != null)
                ApplyPowerUp(player);
        }
    }

    private void ApplyPowerUp(Player player) {
        AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);

        switch (tag) {
            case "Triple_Shoot_PowerUp":
            player.ApplyTripleShootPowerUp();
            break;
            case "Speed_PowerUp":
            player.ApplySpeedPowerUp();
            break;
            case "Shield_PowerUp":

            player.ApplyShieldPowerUp();
            break;
        }

        Destroy(gameObject);
    }
}