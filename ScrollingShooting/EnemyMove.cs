using UnityEngine;

//ȭ���� �Ʒ��� �̵�
//�÷��̾ ����

public class EnemyMove : MonoBehaviour
{
    //�ӵ�
    public float speed = 5f;

    //����
    Vector3 dir;

    //����Ʈ ����
    public GameObject explosionFactory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //�����ϰ� ���ڸ� ����
        int randomNum = Random.Range(0, 10);
        print(randomNum);

        if(randomNum > 3 ) //30% Ȯ��
        {
            //�÷��̾� ������Ʈ ã��
            GameObject player = GameObject.Find("Player");

            //�÷��̾��� ������ ���
            dir = player.transform.position - transform.position;
            dir.Normalize();
        }
        else
        {
            //�ܼ��� �Ʒ��� ��������
            dir = Vector3.down;
        }


    }

    // Update is called once per frame
    void Update()
    {
        //�÷��̾� �߰�
        transform.position += dir * speed * Time.deltaTime;
    }

    //�浹�ÿ� Bullet ����

    private void OnCollisionEnter(Collision collision)
    {
        //������Ʈ�� �ε�����
        GameObject explosion = Instantiate(explosionFactory);
        explosion.transform.position += transform.position;

        //�ε��� ������Ʈ�� Bullet�̶�� ������Ʈ���
        if(collision.gameObject.GetComponent<Bullet>())
        {
            //��Ȱ��ȭ
            collision.gameObject.SetActive(false);
        }
        Destroy(gameObject); //�� �ڽŵ� ����
    }


}
