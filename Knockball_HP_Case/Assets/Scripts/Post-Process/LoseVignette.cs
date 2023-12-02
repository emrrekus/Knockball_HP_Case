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
    [SerializeField] private float duration;

    private void Awake()
    {
        vignette = _postProcessProfile.GetSetting<Vignette>();
        vignette.intensity.value = 0f;
        vignette.smoothness.value = 0f;
    }

    private void Start()
    {
        GameManager.Instance.vignette += Lose;
    }

    private void OnDisable()
    {
        GameManager.Instance.vignette -= Lose;
    }

    public void Lose()
    {
        StartCoroutine(ChangeVignetteOverTime());
    }

    IEnumerator ChangeVignetteOverTime()
    {
        float elapsedTime = 0f;

        var startIntensity = vignette.intensity.value;
        var startSmoothness = vignette.smoothness.value;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            vignette.intensity.value = Mathf.Lerp(startIntensity, 1f, elapsedTime / duration);
            vignette.smoothness.value = Mathf.Lerp(startSmoothness, 1f, elapsedTime / duration);

            yield return null;
        }
    }
}