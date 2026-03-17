using System;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class XpSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject fill;
    [SerializeField] private TMP_Text xpText;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Start()
    {
        slider.minValue = 0;
        slider.maxValue = PlayerStats.Instance.xpUntilLevelUp;
    }

    // Update is called once per frame
    void Update()
    {
        if (slider != null) UpdateSlider();
        UpdateText();
    }

    private void UpdateSlider()
    {
        slider.value = PlayerStats.Instance.currentExperience;
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
        xpText.text = $"{PlayerStats.Instance.currentExperience} / {PlayerStats.Instance.xpUntilLevelUp} XP";
    }

    private void UpdateMinAndMax()
    {
        slider.minValue = 0;
        slider.maxValue = PlayerStats.Instance.xpUntilLevelUp;
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
