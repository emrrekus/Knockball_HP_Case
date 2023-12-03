using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShot : MonoBehaviour
{
    [SerializeField] private float firePower;
    [SerializeField] private BallObjectPooling _ballObjectPooling;
    [SerializeField] private CannonAnim _cannonAnim;


    private bool CanShoot;

    private void Awake()
    {
        _ballObjectPooling = GetComponent<BallObjectPooling>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    
    //Both PC and mobile compatible shoot mechanics. We play shooting mechanics, animations and sounds that should play in shooting mechanics.
    
    void Shoot()
    {
        if (!GameManager.Instance.CanShoot) return;

        var inst = _ballObjectPooling.GetBall();
        inst.transform.position = _ballObjectPooling.SpawnPoint.position;

        Ray ray;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        }
        else
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        Vector3 dir = ray.direction.normalized;

        GameManager.Instance.CurrentBall--;
        _cannonAnim.ShootAnim();
        AudioManager.Instance.PlayOneShotClip(0);

        if (inst.TryGetComponent<Ball>(out var ball))
        {
            ball.Rigibody.isKinematic = false;
            ball.Rigibody.AddForce(dir * firePower, ForceMode.Impulse);
        }
      
    }
}