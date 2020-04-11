using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] float _speed = 8f;
    private float yBoundary = 8f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y >= yBoundary)
        {
            Destroy(this.gameObject);
        }
    }
}
