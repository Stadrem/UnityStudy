using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInventory : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField]
    private GameObject inventoryPanel;      // 인벤토리
    [SerializeField]
    private GameObject IPanel;      // I키 UI
    [SerializeField]
    private GameObject detailsPanel;        // 아이템 상세 설명
    [SerializeField]
    private UIItem[] items;             // 인벤토리에 있는 아이템 정보
    [SerializeField]
    private PlayerHP playerHP;
    [SerializeField]
    private PlayerStamina playerSP;

    [Header("Current Item")]
    [SerializeField]
    private Image currentItemIcon;
    [SerializeField]
    private TextMeshProUGUI currentItemName;
    [SerializeField]
    private TextMeshProUGUI currentItemCount;
    [SerializeField]
    private TextMeshProUGUI currentItemDetails;

    private UIItem currentItem;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            IPanel.SetActive(!IPanel.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && detailsPanel.activeSelf == true)
        {
            detailsPanel.SetActive(false);
        }
    }

    public void GetItem(int index)
    {
        items[index].Count ++;
    }

    public void UpdateCurrentItem(UIItem current)
    {
        detailsPanel.SetActive(true);

        currentItem = current;

        currentItemIcon.sprite = currentItem.Icon;
        currentItemName.text = currentItem.Name;
        currentItemCount.text = $"{currentItem.Count}/9999";
        currentItemDetails.text = currentItem.Details;
    }

    public void OnclikUse()
    {
        if (currentItem.Count > 0)
        {
            currentItem.Count--;
            playerHP.Current += currentItem.HPRecovery;
            playerSP.Current += currentItem.SPRecovery;
        }
    }
}
