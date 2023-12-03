using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ExplodeCube : Cube
{
    [SerializeField] private float explosionForce;
    [SerializeField] private float explosionRadius;
    [SerializeField] private ParticleSystem _explodePartical;
    [SerializeField] private float _shakeDuration;
    [SerializeField] private float _shakeStrenght;
    

    private void Start()
    {
        _explodePartical.Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            GameManager.Instance.CameraShake(_shakeDuration, _shakeStrenght);
            AudioManager.Instance.PlayOneShotClip(1);
            _explodePartical.Play();
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