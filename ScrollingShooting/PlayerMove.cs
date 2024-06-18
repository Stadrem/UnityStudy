using UnityEngine;
//플레이어를 이동시키는 기능
//사용자(유저) 입력에 따라서 이동

public class PlayerMove : MonoBehaviour
{
    //속도를 조절할 수 있는 전역변수
    public float speed = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //사용자 입력에 맞춰서
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //방향을 설정
        Vector3 dir = new Vector3(h,v,0);
        dir.Normalize(); //방향은 유지, 단위벡터 변환
        //설정된 방향에 맞춰서 오브젝트를 이동
        transform.position += dir * speed * Time.deltaTime;
    }
}
