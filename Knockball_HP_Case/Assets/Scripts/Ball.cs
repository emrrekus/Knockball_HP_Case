using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
   
   [SerializeField] private Rigidbody rb;
   [SerializeField] private float _lifetime;
   
   private float _spawnTime;
   public Rigidbody Rigibody => rb;
   
   public event Action DestroyRequested;
   

   private void Start()
   {
     
      InvokeRepeating("UpdateKinematic",5f, 5f);
   }

   public void ResetSpawnTime()
   {
      _spawnTime = Time.time;
   }
   private void Awake()
   {
      rb = GetComponent<Rigidbody>();
      rb.isKinematic = true;
      ResetSpawnTime();
   }

   private void Update()
   {
      if (_lifetime > 0 && Time.time - _spawnTime> _lifetime)
      {
         DestroyRequested?.Invoke();
         return;
      }
   }

   private void UpdateKinematic()
   {
      
      rb.isKinematic = true;
   }



   public void GameObjectSetActive(bool setactive)
   {
      this.gameObject.SetActive(setactive);
   }
}
