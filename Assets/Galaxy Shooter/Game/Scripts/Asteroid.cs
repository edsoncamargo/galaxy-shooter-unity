using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    [SerializeField] private float speed = 2.0f;

    private float initialVerticalArea = 6f;
    private float minHorizontalArea = -8.0f;
    private float maxHorizontalArea = 8.0f;

    private Animator _animator;
    AnimationClip clip;

    private bool isExplosion = false;

    void Start() {
        _animator = GetComponent<Animator>();
        clip = _animator.runtimeAnimatorController.animationClips[0];
        Respawn();
    }

    private void Respawn() {
        float randomHorizontalArea = Random.Range(minHorizontalArea, maxHorizontalArea);
        transform.position = new Vector3(randomHorizontalArea, initialVerticalArea, 0);
    }

    void Update() {
        Move();
        CheckDestroy();
    }

    private void Move() {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }

    private void CheckDestroy() {
        if (transform.position.y < -6f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && isExplosion == false) {
            other.GetComponent<Player>().Demage();
            _animator.Play("Asteroid_Explosion");
            isExplosion = true;
            Destroy(gameObject, clip.length);
        }

        if (other.CompareTag("Laser") && isExplosion == false) {
            _animator.Play("Asteroid_Explosion");
            isExplosion = true;
            Destroy(gameObject, clip.length);
        }
    }
}
