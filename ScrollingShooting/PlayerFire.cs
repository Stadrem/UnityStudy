using UnityEngine;
//fire1�� Ŭ�� ���� ��
//�̻����� �߻�

public class PlayerFire : MonoBehaviour
{
    //�ֹ���
    public GameObject bulletFactory;

    //�̻����� ��ġ�� ������ ����Ʈ
    public GameObject firePosition;

    //�̻����� ���� �� �ִ� �迭
    public GameObject[] bulletArray;

    //��ĭ�� ����
    public int bulletLength = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //�迭�� �̿��ؼ� ������ Ȯ��.
        bulletArray = new GameObject[bulletLength];
        //����ִ� �迭�� ä�� (�ݺ���)
        for(int i = 0; i < bulletLength; i++)
        {
            //�Ѿ��� ����
            GameObject go = Instantiate(bulletFactory);

            //�Ѿ��� ä������
            bulletArray[i] = go;

            //����ġ�� ��
            go.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //������ Fire1�� Ŭ���ϸ�
        if (Input.GetButtonDown("Fire1"))
        {
            //��Ȱ��ȭ �Ǿ��ִ� Bullet ã��
            for (int i = 0; i < bulletLength; i++) 
            {
                //bulletArray[i]��° �Ѿ��� ��Ȱ��ȭ �Ǿ��ִٸ�
                if (bulletArray[i].activeSelf == false)
                {
                    //firePosition ��ġ�� �̵�
                    bulletArray[i].transform.position = firePosition.transform.position;
                    //Ȱ��ȭ
                    bulletArray[i].SetActive(true);
                    //�ݺ��� ����
                    break;
                }
            }
        }
    }
}
