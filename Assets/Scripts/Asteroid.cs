using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] float _rotateSpeed = 3.0f;
    [SerializeField] GameObject _explosionPrefab;
    private Animator _explosionAnim;

    // Start is called before the first frame update
    void Start()
    {
        _explosionAnim = _explosionPrefab.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3.forward = new Vector3(0, 0, 1)
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(this.gameObject, 0.25f);
        }
    }
}
