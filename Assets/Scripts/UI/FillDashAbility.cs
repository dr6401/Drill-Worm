using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FillDashAbility : MonoBehaviour
{
    public Image fillImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fillImage ==null) return;
        if (Movement.Instance.isDashing)
        {
            fillImage.fillAmount = 0;
        }
        else
        {
            fillImage.fillAmount = Mathf.Clamp01(Movement.Instance.timeSinceLastDash / Movement.Instance.dashCooldown);   
        }
    }
}
