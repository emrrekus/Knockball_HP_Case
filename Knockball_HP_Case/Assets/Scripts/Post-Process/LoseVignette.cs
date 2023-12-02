using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;


public class LoseVignette : MonoBehaviour
{
    [SerializeField] private PostProcessProfile _postProcessProfile;
    [SerializeField] private Vignette vignette;
    [SerializeField] private float _durationOpen;
    [SerializeField] private float _durationClose;

    

    private void Awake()
    {
        vignette = _postProcessProfile.GetSetting<Vignette>();
        vignette.intensity.value = 0f;
        vignette.smoothness.value = 0f;
    }

    private void Start()
    {
    
    }

    private void OnEnable()
    {
        GameManager.Instance.vignetteOpen += VignetteOpen;
        GameManager.Instance.vignetteClose += VignetteClose;
    }

    private void OnDisable()
    {
        GameManager.Instance.vignetteOpen -= VignetteOpen;
        GameManager.Instance.vignetteClose += VignetteClose;
    }

    public void VignetteOpen()
    {
      StartCoroutine(ChangeVignetteOverTime());
    }

    public void VignetteClose()
    {
        StartCoroutine(ChangeVignetteOverTimeReverse());
    }

    IEnumerator ChangeVignetteOverTime()
    {
      
        float elapsedTime = 0f;

        var startIntensity = vignette.intensity.value;
        var startSmoothness = vignette.smoothness.value;  
        
        while (elapsedTime < _durationOpen)
        {
            elapsedTime += Time.deltaTime;

            vignette.intensity.value = Mathf.Lerp(startIntensity, 1f, elapsedTime / _durationOpen);
            vignette.smoothness.value = Mathf.Lerp(startSmoothness, 1f, elapsedTime / _durationOpen);

            yield return null;
        }
    }
    
    IEnumerator ChangeVignetteOverTimeReverse()
    {
       
        float elapsedTime = 0f;

        var startIntensity = vignette.intensity.value;
        var startSmoothness = vignette.smoothness.value;  

        while (elapsedTime < _durationClose)
        {
            elapsedTime += Time.deltaTime;

            vignette.intensity.value = Mathf.Lerp(startIntensity, 0f, elapsedTime / _durationClose);
            vignette.smoothness.value = Mathf.Lerp(startSmoothness, 0f, elapsedTime / _durationClose);

            yield return null;
        }

        
    }
    
    
  
}