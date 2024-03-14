using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;

    private void Update()
    {
        MovePowerUp();
        CheckDestroy();
    }

    private void MovePowerUp()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }

    private void CheckDestroy()
    {
        if (transform.position.y < -6f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
                ApplyPowerUp(player);
        }
    }

    private void ApplyPowerUp(Player player)
    {
        switch (tag)
        {
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