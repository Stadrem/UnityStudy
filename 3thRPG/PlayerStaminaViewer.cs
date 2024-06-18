using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStaminaViewer : MonoBehaviour
{
    [SerializeField]
    private PlayerStamina playerStamina;
    [SerializeField]
    private TextMeshProUGUI textStamina;
    private Slider sliderStamina;

    private void Awake()
    {
        sliderStamina = GetComponent<Slider>();
    }

    private void Update()
    {
        sliderStamina.value = playerStamina.Current / playerStamina.Max;
        textStamina.text = $"���¹̳� {playerStamina.Current:F0}/{playerStamina.Max}";
    }

}
