using UnityEngine;

//�̰Ͱ� �ε��� ������Ʈ�� ����

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //�ε��� ������Ʈ�� Bullet�̶��
        if (other.gameObject.name.Contains("Bullet"))
        {
            //��Ȱ��ȭ
            other.gameObject.SetActive(false);
        }
        else
        {
            //�ε��� ������Ʈ�� ����
            Destroy(other.gameObject);
        }
    }
}
