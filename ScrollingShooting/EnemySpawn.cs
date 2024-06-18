using UnityEngine;

//���� �ð��� ������ ���ʹ̸� �����ض�

public class EnemySpawn : MonoBehaviour
{
    //���ʹ� prefab
    public GameObject enemyFactory;

    //������ �ð�
    public float createTime = 2f;
    float currentTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //�����ϰ� ����ð��� 2~5�� ���̷�
        createTime = Random.Range(2f, 5f);
        //���� �ð��� 0���� �ʱ�ȭ
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //����ð����� ����ð��� �۴ٸ�
        //�ð��� ������Ų��
        if (currentTime < createTime) 
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            //���� �ð��� ����ð����� ���ų� Ŀ���ٸ�
            //enemy�� �����ض�
            GameObject go = Instantiate(enemyFactory);
            //����� ���ʹ��� ��ġ�� ���� ��ġ�� �̵�
            go.transform.position = transform.position;
            //�����ϰ� ����ð��� 2~5�� ���̷�
            createTime = Random.Range(2f, 5f);
            //���� �ð��� 0���� �ʱ�ȭ
            currentTime = 0;
        }
    }
}
