using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float _speed = 4.0f;
    private float _yStartPos = 7f;
    private float _yBoundary = -5f;
    private float _xRange = 8f;

    private PlayerController _player;
    private Animator _enemyDestroyAnim;

    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerController>();
        _enemyDestroyAnim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        if (_player == null)
        {
            Debug.LogError("Player is null");
        }

        if (_enemyDestroyAnim == null)
        {
            Debug.LogError("Animator is null");
        }

        if (_audioSource == null)
        {
            Debug.LogError("Audio Source on enemy is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < _yBoundary)
        {
            float randomXPos = Random.Range(-_xRange, _xRange);
            transform.position = new Vector3(randomXPos, _yStartPos, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.transform.GetComponent<PlayerController>();

            if (player != null)
            {
                player.Damage();
            }

            _enemyDestroyAnim.SetTrigger("OnEnemyDeath");
            _audioSource.Play();
            _speed = 0;
            Destroy(this.gameObject, 2.7f);
        }
        else if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddScore(10);
            }

            _enemyDestroyAnim.SetTrigger("OnEnemyDeath");
            _audioSource.Play();
            _speed = 0;
            Destroy(this.gameObject, 2.7f);
        }
    }
}
