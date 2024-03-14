using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10.0f;

    void Start()
    {
        
    }

    void Update()
    {
        HandleDispatch();
    }

    private void HandleDispatch()
    {
        MoveUp();
        HandleDestroy();
    }

    private void MoveUp()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _speed);
    }

    private void HandleDestroy()
    {
        
        if (transform.position.y >= 6)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            {
                EnemyAI enemyAI = other.GetComponent<EnemyAI>();
                if (enemyAI == null) return;

                Destroy(gameObject);
            }
    }
}
