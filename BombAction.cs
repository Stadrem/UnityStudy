using UnityEngine;

public class BombAction : MonoBehaviour
{
    //���� ȿ�� ������
    public GameObject explosionFactory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //����ȿ�� ���忡�� ����ȿ���� ����
        GameObject explo = Instantiate(explosionFactory);

        //������ ����ȿ���� ���� ��ġ�� ��ġ
        explo.transform.position = transform.position;

        //���� �ı�
        Destroy(gameObject);
    }
}
