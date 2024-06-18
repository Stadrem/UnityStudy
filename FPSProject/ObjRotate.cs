using UnityEngine;

//마우스의 움직임에 따라
//카메라, Player를 회전
public class ObjRotate : MonoBehaviour
{
    //회전값(마우스의 움직임을 누적하는 값)
    float rotX = 0;
    float rotY = 0;

    //회전 스피드값
    float rotSpeed = 150;

    //회전 가능 여부
    public bool useVertical = false;
    public bool useHorizontal = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //1. 마우스의 움직임 값을 받아오기
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        //2. 마우스의 움직임 값을 누적
        if(useHorizontal == true)
        {
            rotY += mx * Time.deltaTime * rotSpeed;
        }
        
        if(useVertical == true)
        {
            rotX += my * Time.deltaTime * rotSpeed;
        }
        
        //3. 누적된 값을 물체의 회전값으로 세팅
        transform.localEulerAngles = new Vector3(-rotX, rotY, 0);
    }
}
