using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIMinimap : MonoBehaviour
{
    [SerializeField]
    private Camera minimapCamera;
    [SerializeField]
    private float zoomMin = 1.0f;
    [SerializeField]
    private float zoomMax = 30.0f;
    [SerializeField]
    private float zoomStepSize = 1.0f;
    [SerializeField]
    private TextMeshProUGUI textMapInfo;

    private void Awake()
    {
        //textMapInfo.text = SceneManager.GetActiveScene().name;
        textMapInfo.text = "±Ì¿∫ Ω£";
    }

    public void ZoomIn()
    {
        minimapCamera.orthographicSize = Mathf.Max(minimapCamera.orthographicSize - zoomStepSize, zoomMin);
    }

    public void ZoomOut()
    {
        minimapCamera.orthographicSize = Mathf.Min(minimapCamera.orthographicSize + zoomStepSize, zoomMax);
    }
}

