using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeCube : MonoBehaviour
{
    [SerializeField] private float explosionForce;
    [SerializeField] private float explosionRadius;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Explode();
        }
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (var cube in colliders)
        {
            Rigidbody rb = cube.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, 3.0f, ForceMode.Impulse);
            }
        }
    }
}