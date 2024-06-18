using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    //Bomb ����(������)
    public GameObject bombFactory;

    //�߻� ��ġ
    public GameObject firePos;

    //�߻� ��
    public float firePower = 100;

    //���� ȿ�� ����(������)
    public GameObject bulletImpactFactory;

    float myTime = 0f; // �ʱ�ȭ
    float nextFire = 0.25f; // 0.1�� ������

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //1. ���콺 ������ ��ư�� ������
        if (Input.GetButtonDown("Fire2"))
        {
            //2. Bomb ���忡�� Bomb�� �ϳ� ����
            GameObject bomb = Instantiate(bombFactory);

            //3. ������ Bomb�� ��ġ�� firePos�� ��ġ
            bomb.transform.position = firePos.transform.position;

            //4. ������ Bomb�� ī�޶��� �չ������� ���ư� �� �ְ� ���� �ش�.
            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            rb.AddForce(Camera.main.transform.forward * firePower);
        }

        myTime += Time.deltaTime; // �ð� ������Ʈ

        //1. ���콺 ���� ��ư�� ������
        if (Input.GetButton("Fire1") && myTime > nextFire)
        {
            // myTime�� �ʱ�ȭ�Ͽ� �����̸� �缳��
            myTime = 0f;

            //2. ī�޶� ��ġ, ī�޶� �չ��� Ray�� ����.
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            //3. Ray�� �߻��ؼ� ��򰡿� �¾Ҵٸ�
            RaycastHit hitInfo = new RaycastHit();

            if(Physics.Raycast(ray, out hitInfo))
            {
                //4. ���� ��ġ�� ����ȿ���� �����ش�.
                //Debug.Log("���� ������Ʈ : " + hitInfo.transform.name);
                //4-1 ���� ȿ�� ���忡�� ���� ȿ�� �����
                GameObject bulletImpact = Instantiate(bulletImpactFactory);

                //4-2 ���� ���� ȿ���� ���� ��ġ�� ����
                bulletImpact.transform.position = hitInfo.point;

                //4-3 ���� ���� ȿ���� �� ������ ���� ��ġ�� Normal ������ ����
                bulletImpact.transform.forward = hitInfo.normal;

                //4-4 ���� ���� ȿ���� 2�� �ڿ� �ı�
                Destroy(bulletImpact, 1);

                //5. ���� ����� Enemy
                if (hitInfo.transform.gameObject.name.Contains("Enemy"))
                {
                    //6. Enemy���� Damage ���ϱ�
                    //6-1. Enemy���� Enemy ������Ʈ�� ��������
                    Enemy enemy = hitInfo.transform.GetComponent<Enemy>();

                    //6-2. ������ ������Ʈ���� OnDamaged �Լ��� ȣ��
                    enemy.OnDamaged();
                }
            }
        }
    }
}
