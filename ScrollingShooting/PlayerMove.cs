using UnityEngine;
//�÷��̾ �̵���Ű�� ���
//�����(����) �Է¿� ���� �̵�

public class PlayerMove : MonoBehaviour
{
    //�ӵ��� ������ �� �ִ� ��������
    public float speed = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //����� �Է¿� ���缭
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //������ ����
        Vector3 dir = new Vector3(h,v,0);
        dir.Normalize(); //������ ����, �������� ��ȯ
        //������ ���⿡ ���缭 ������Ʈ�� �̵�
        transform.position += dir * speed * Time.deltaTime;
    }
}
