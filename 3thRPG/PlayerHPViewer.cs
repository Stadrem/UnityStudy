using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHPViewer : MonoBehaviour
{
    [SerializeField]
    private PlayerHP playerHP;
    [SerializeField]
    private TextMeshProUGUI textHP;
    private Slider sliderHP;

    private void Awake()
    {
        sliderHP = GetComponent<Slider>();
    }

    private void Update()
    {
        sliderHP.value = playerHP.Current / playerHP.Max;
        textHP.text = $"Ã¼·Â {(int)playerHP.Current}/{playerHP.Max}";
    }
}
