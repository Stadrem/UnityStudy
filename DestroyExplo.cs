using UnityEngine;

//2�ʰ� ������ �� �ڽ��� �ı�

public class DestroyExplo : MonoBehaviour
{
    //����ð�
    float currentTime = 0;

    //�ı��ð�
    float destroyTime = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //1. ���� �ð��� ����
        currentTime += Time.deltaTime;

        //2. ���� �ð��� �ı� �ð����� Ŀ����
        if (currentTime > destroyTime)
        {
            //3. ���� �ı�
            Destroy(gameObject);
        }

    }
}
