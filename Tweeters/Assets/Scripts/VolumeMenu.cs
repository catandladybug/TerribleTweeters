using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeMenu : MonoBehaviour
{

    [SerializeField] string _volumeParameter = "MusicVolume";
    [SerializeField] string _volumeParameter2 = "MasterVolume";
    [SerializeField] AudioMixer _mixer;
    [SerializeField] Slider _slider;
    [SerializeField] float _multiplier = 30f;

    [SerializeField] Toggle _toggle;

    private bool _disableToggleEvent;

    private void Awake()
    {

        _slider.onValueChanged.AddListener(HandleValue);
        _toggle.onValueChanged.AddListener(HandleToggle);

    }

    void HandleToggle(bool enableSound)
    {
        if(_disableToggleEvent)
        {

            return;

        }
        
        if (enableSound)
        {

            _slider.value = _slider.maxValue;

        }

        else
        {

            _slider.value = _slider.minValue;

        }

    }

    private void OnDisable()
    {

        PlayerPrefs.SetFloat(_volumeParameter, _slider.value);
        PlayerPrefs.SetFloat(_volumeParameter2, _slider.value);

    }

     void HandleValue(float value)
    {

        _mixer.SetFloat(_volumeParameter, Mathf.Log10(value) * _multiplier);
       _mixer.SetFloat(_volumeParameter2, Mathf.Log10(value) * _multiplier);

        _disableToggleEvent = true;
        _toggle.isOn = _slider.value > _slider.minValue;
        _disableToggleEvent = false;

    }

    void Start()
    {
        
        _slider.value = PlayerPrefs.GetFloat(_volumeParameter, _slider.value);
        _slider.value = PlayerPrefs.GetFloat(_volumeParameter2, _slider.value);

    }

}
