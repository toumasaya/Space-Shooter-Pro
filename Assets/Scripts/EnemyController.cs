using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float _speed = 4.0f;
    private float yStartPos = 7f;
    private float yBoundary = -5f;
    private float xRange = 8f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, yStartPos, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < yBoundary)
        {
            float randomXPos = Random.Range(-xRange, xRange);
            transform.position = new Vector3(randomXPos, yStartPos, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.transform.GetComponent<PlayerController>();

            if (player != null)
            {
                player.Damage();
            }

            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
