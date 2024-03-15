using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    [SerializeField]
    private GameObject _laserPrefab;
    private float _fireRate = 0.25f;
    private float _nextFire = 0.0f;
    [SerializeField]
    private GameObject _tripleLaserPrefab;
    private string power = "single";

    private bool _hasShield = false;
    [SerializeField]
    private GameObject _shieldGameObject;

    private float _initialSpeed = 5.0f;
    private float _speed = 0.0f;
    private float _horizontalInput = 0.0f;
    private float _verticalInput = 0.0f;

    [SerializeField]
    private int lifes = 3;

    [SerializeField]
    private GameObject _explosionPrefab;

    private UiManager _uiManager;
    private GameManager _gameManager;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip _clip;

    [SerializeField]
    private GameObject[] _fireHurtGameObject;

    void Start() {
        Setup();

        _uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (_uiManager != null) {
            _uiManager.UpdateLives(lifes);
        }

        _audioSource = GetComponent<AudioSource>();
    }


    void Setup() {
        _speed = _initialSpeed;
    }

    void Update() {
        if (IsAlive()) {
            HandleMovements();
            HandleLaser();
            return;
        }

        Explode();
    }

    private void HandleMovements() {
        HandleHorizontalMove();
        HandleVerticalMove();
        HandlePlayerBoundsX();
        HandlePlayerBoundY();
    }

    private void HandleHorizontalMove() {
        _horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Time.deltaTime * _speed * _horizontalInput);
    }

    private void HandleVerticalMove() {
        _verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * Time.deltaTime * _speed * _verticalInput);
    }

    private void HandlePlayerBoundsX() {
        if (transform.position.x > 10f) {
            transform.position = new Vector3(-10f, transform.position.y, 0);
        } else if (transform.position.x < -10f) {
            transform.position = new Vector3(10f, transform.position.y, 0);
        }
    }

    private void HandlePlayerBoundY() {
        if (transform.position.y > 0) {
            transform.position = new Vector3(transform.position.x, 0, 0);
        } else if (transform.position.y < -4.2f) {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }
    }

    private void HandleLaser() {
        HandleShoots();
    }

    private void HandleShoots() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (HandleFireCooldown()) {
                HandleShoot();
            }
        }
    }

    private bool HandleFireCooldown() {
        bool canFire = Time.time > _nextFire;

        if (canFire) {
            _nextFire = Time.time + _fireRate;
        }

        return canFire;
    }

    private void HandleShoot() {
        _audioSource.Play();

        switch (power) {
            case "single":
            SingleShoot();
            break;
            case "triple":
            TripleShoots();
            break;
        }
    }

    private void SingleShoot() {
        Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + 0.88f, transform.position.z), Quaternion.identity);
    }

    private void TripleShoots() {
        Instantiate(_tripleLaserPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
    }

    public void ApplyTripleShootPowerUp() {
        power = "triple";
        StartCoroutine(RevokeTripleShootPowerUp());
    }

    IEnumerator RevokeTripleShootPowerUp() {
        yield return new WaitForSeconds(5.0f);
        power = "single";
    }

    public void ApplySpeedPowerUp() {
        _speed = _speed + 10.0f;
        StartCoroutine(RevokeSpeedPowerUp());
    }

    IEnumerator RevokeSpeedPowerUp() {
        yield return new WaitForSeconds(5.0f);
        _speed = _initialSpeed;
    }

    public void ApplyShieldPowerUp() {
        _hasShield = true;
        _shieldGameObject.SetActive(true);
        StartCoroutine(RevokeShieldPowerUp());
    }

    IEnumerator RevokeShieldPowerUp(float timer = 5.0f) {
        yield return new WaitForSeconds(timer);
        _hasShield = false;
        _shieldGameObject.SetActive(false);
    }

    public void HandleCollidedWithTheEnemyShip() {
        if (_hasShield) {
            StartCoroutine(RevokeShieldPowerUp(0.0f));
            return;
        }


        if (IsAlive()) {
            lifes--;

            if (_uiManager != null) {
                _uiManager.UpdateLives(lifes);
            }

            if (lifes > 0 && lifes < 3) {
                _fireHurtGameObject[lifes == 2 ? 0 : 1].SetActive(true); 
            }

            return;
        }
    }

    public bool IsAlive() {
        return lifes > 0;
    }

    public void Explode() {
        AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

        if (_gameManager != null) {
            _gameManager.GameOver();
        }

        Destroy(gameObject);
    }
}