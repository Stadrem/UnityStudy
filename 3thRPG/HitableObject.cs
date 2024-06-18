using System.Collections;
using UnityEngine;

public class HitableObject : MonoBehaviour
{
    [SerializeField]
    private GameObject[] dropObjects;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    [Range(0, 100)]
    private int itemDropPercent = 0;
    private MeshRenderer meshRenderer;
    private Color originColor;

    // �߰�: Ÿ�� �� Z�� ������ �ִ� ������
    private float zVibrationDistance = 0.05f; // Z�� ���� �Ÿ�
    private int zVibrationCount = 2; // ���� Ƚ��
    private float zVibrationDuration = 0.1f; // ���� ���� �ð�

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originColor = meshRenderer.material.color;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"{damage}�� ���ظ� �Ծ����ϴ�.");

        StartCoroutine(nameof(OnHitAnimation));

        if(itemDropPercent != 0 && dropObjects.Length != 0)
        {
            int percent = Random.Range(0, 100);
            if(percent < itemDropPercent )
            {
                int index = Random.Range(0, dropObjects.Length);
                GameObject clone = Instantiate(dropObjects[index]);

                clone.transform.position = spawnPoint.position;
                clone.transform.localScale = new Vector3(5, 5, 5);
            }
        }
    }

    private IEnumerator OnHitAnimation()
    {
        // Ÿ�� �� Z�� ����
        for (int i = 0; i < zVibrationCount; i++)
        {
            transform.Translate(Vector3.left * zVibrationDistance);
            yield return new WaitForSeconds(zVibrationDuration / 2);
            transform.Translate(Vector3.right * zVibrationDistance);
            yield return new WaitForSeconds(zVibrationDuration / 2);
        }

        meshRenderer.material.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        meshRenderer.material.color = originColor;
    }
}

