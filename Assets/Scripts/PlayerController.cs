using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed = 3.5f;
    private float _speedMultiplier = 2;

    [SerializeField] GameObject _laserPrefab;
    [SerializeField] GameObject _tripleShotPrefab;
    [SerializeField] GameObject _shieldsVisualizer;
    [SerializeField] GameObject _leftEngine, _rightEngine;
    [SerializeField] AudioClip _laserSoundClip;
    private AudioSource _audioSource;

    [SerializeField] float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField] int _lives = 3;

    private SpawnManager _spawnManager;
    private UIManager _uiManager;

    [SerializeField] bool _isTripleShotActive = false;
    [SerializeField] bool _isSpeedBoostActive = false;
    [SerializeField] bool _isShieldsActive = false;
    [SerializeField] int _score;

    // Start is called before the first frame update
    void Start()
    {
        // take the current position = new position (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();


        if (_spawnManager == null)
        {
            Debug.LogError("SpawnManager is null");
        }

        if (_uiManager == null)
        {
            Debug.LogError("UI Manager is null");
        }

        if (_audioSource == null)
        {
            Debug.LogError("Audio Source on player is null");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        float currentXPos = transform.position.x;
        float currentYPos = transform.position.y;
        float yBoundary = -3.8f;
        float xBoundary = 11f;

        transform.position = new Vector3(currentXPos, Mathf.Clamp(currentYPos, yBoundary, 0), 0);

        if (transform.position.x > xBoundary)
        {
            transform.position = new Vector3(-xBoundary, transform.position.y, 0);
        }
        else if (transform.position.x < -xBoundary)
        {
            transform.position = new Vector3(xBoundary, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Vector3 _offset = transform.position + new Vector3(0, 1.05f, 0);
            Instantiate(_laserPrefab, _offset, Quaternion.identity);
        }

        _audioSource.Play();

    }

    public void Damage()
    {
        if (_isShieldsActive == true)
        {
            _isShieldsActive = false;
            _shieldsVisualizer.SetActive(false);
            return;
        }

        _lives--;

        if (_lives == 2)
        {
            _leftEngine.SetActive(true);
        }
        else if (_lives == 1)
        {
            _rightEngine.SetActive(true);
        }

        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _spawnManager.onPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerdownRoutine());
    }

    IEnumerator SpeedBoostPowerdownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }

    public void ShieldsActive()
    {
        _isShieldsActive = true;
        _shieldsVisualizer.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
