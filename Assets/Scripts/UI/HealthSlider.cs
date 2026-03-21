using System;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject fill;
    [SerializeField] private TMP_Text hpText;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Start()
    {
        slider.minValue = 0;
        slider.maxValue = PlayerStats.Instance.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (slider != null) UpdateSlider();
        UpdateText();
    }

    private void UpdateSlider()
    {
        slider.value = PlayerStats.Instance.currentHealth;
        slider.maxValue = PlayerStats.Instance.maxHealth;
        if (slider.value <= 0)
        {
            if (fill.gameObject.activeSelf) fill.SetActive(false);
        }
        else 
        {
            if (!fill.gameObject.activeSelf) fill.SetActive(true);
        }
        //Debug.Log($"Updating slider at {slider.value}");
    }

    private void UpdateText()
    {
        hpText.text = $"{PlayerStats.Instance.currentHealth} / {PlayerStats.Instance.maxHealth} HP";
    }

    private void UpdateMinAndMax()
    {
        slider.minValue = 0;
        slider.maxValue = PlayerStats.Instance.maxHealth;
    }

    private void OnEnable()
    {
        GameEvents.OnLevelUp += UpdateMinAndMax;
    }
    
    private void OnDisable()
    {
        GameEvents.OnLevelUp -= UpdateMinAndMax;
    }
}