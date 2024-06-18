using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;

    //캐릭터 컨트롤러 컴포넌트
    CharacterController cc;

    //중력
    float gravity = -20;

    //수직속력
    float yVelocity = 0;

    //점프파워
    float jumpPower = 5;

    //HP Slider
    public Slider hpSlider;

    //HP Text
    public Text hpText;

    //현재 HP
    float currentHP = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 캐릭터 컨트롤러 컴포넌트 불러오기
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //1. 사용자의 입력을 받자 (w,s,a,d)
        float h = Input.GetAxis("Horizontal"); // a: -1, d: 1 누르지 않으면 0
        float v = Input.GetAxis("Vertical"); // s: -1, w: 1 누르지 않으면 0

        //2. 방향을 만듬
        Vector3 dirH = transform.right * h;
        Vector3 dirV = transform.forward * v;
        Vector3 dir = dirH + dirV;

        //만약에 스페이스바를 누르면
        if (Input.GetButtonDown("Jump"))
        {
            //yVelocity를 jumpPower 한다
            yVelocity = jumpPower;
        }
        //yVelocity 값을 점점 줄여준다. (gravity에 의해서)
        yVelocity += gravity * Time.deltaTime;

        //dir의 y값에 yVelocity를 세팅한다
        dir.y = yVelocity;

        //3. 그 방향으로 이동
        //p = p0 + vt
        //transform.position = transform.position + dir * speed * Time.deltaTime;

        cc.Move(dir * speed * Time.deltaTime);
    }

    public void OnDamaged()
    {
        //현재 HP를 줄인다
        currentHP -= 10;
        //HP UI를 갱신한다
        //1. text 갱신
        hpText.text = "HP : " + currentHP;

        //2. slider 갱신
        float ratio = currentHP / 100;
        hpSlider.value = ratio;
    }
}
