using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ExplodeCube : Cube
{
    [SerializeField] private float explosionForce;
    [SerializeField] private float explosionRadius;
    [SerializeField] private float _shakeDuration;
    [SerializeField] private float _shakeStrenght;
    [SerializeField] private AudioClip _explodeClip;
    [SerializeField] private GameObject _expolodeParticalGO;
    [SerializeField] private ParticleSystem _explodePartical;


    protected override void OnAwake()
    {
        ParticleSystem();
    }

   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Ball>(out var ball))
        {
            _gameManagerInstance.CameraShake(_shakeDuration, _shakeStrenght);
            _audioManagerInstance.PlayOneShotClip(_explodeClip);
            _explodePartical.Play();
            _expolodeParticalGO.SetActive(true);
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

    private void ParticleSystem()
    {
        _expolodeParticalGO.SetActive(false);
        _explodePartical.Stop();
    }
}