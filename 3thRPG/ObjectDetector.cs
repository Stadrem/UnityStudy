using UnityEngine;
using TMPro;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField]
    private GameObject respawnPrefab;
    [SerializeField]
    private Transform canvasTransform;
    [SerializeField]
    private TextMeshProUGUI textInteraction;
    private InteractableObject interactable;
    private string interactableName;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && interactable != null)
        {
            SetupRespawnUI();

            interactable.Deactivate();
            interactable = null;
            textInteraction.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            interactable = other.GetComponent<InteractableObject>();

            if (textInteraction.enabled == false)
            {
                textInteraction.enabled = true;

                interactableName = other.name.Split('_')[0];
                textInteraction.text = $"[ {interactableName} ]\nF키를 눌러 채집.";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            if (textInteraction.enabled == true)
            {
                textInteraction.enabled = false;
            }
        }
    }

    private void SetupRespawnUI()
    {
        GameObject clone = Instantiate(respawnPrefab);
        clone.transform.SetParent(canvasTransform);
        clone.transform.localScale = Vector3.one;

        // UI가 쫓아다닐 대상을 방금 비활성화한 상호작용 오브젝트로 설정
        clone.GetComponent<UIPositionAutoSetter>().Setup(interactable.transform);
        // UI에 방금 상호작용한 오브젝트의 이름과 Respawn 시간 전달
        clone.GetComponent<UIRespawn>().OnRespawn(interactableName, interactable.RespawnTime);
    }
}