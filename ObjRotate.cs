using UnityEngine;

//���콺�� �����ӿ� ����
//ī�޶�, Player�� ȸ��
public class ObjRotate : MonoBehaviour
{
    //ȸ����(���콺�� �������� �����ϴ� ��)
    float rotX = 0;
    float rotY = 0;

    //ȸ�� ���ǵ尪
    float rotSpeed = 150;

    //ȸ�� ���� ����
    public bool useVertical = false;
    public bool useHorizontal = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //1. ���콺�� ������ ���� �޾ƿ���
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        //2. ���콺�� ������ ���� ����
        if(useHorizontal == true)
        {
            rotY += mx * Time.deltaTime * rotSpeed;
        }
        
        if(useVertical == true)
        {
            rotX += my * Time.deltaTime * rotSpeed;
        }
        
        //3. ������ ���� ��ü�� ȸ�������� ����
        transform.localEulerAngles = new Vector3(-rotX, rotY, 0);
    }
}
