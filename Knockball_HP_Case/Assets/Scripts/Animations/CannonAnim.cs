using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonAnim : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private static readonly int İsShot = Animator.StringToHash("isShot");


    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ShootAnim()
    {
        _animator.SetTrigger(İsShot);
    }
}
