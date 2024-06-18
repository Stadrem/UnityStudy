using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;

    //ĳ���� ��Ʈ�ѷ� ������Ʈ
    CharacterController cc;

    //�߷�
    float gravity = -20;

    //�����ӷ�
    float yVelocity = 0;

    //�����Ŀ�
    float jumpPower = 5;

    //HP Slider
    public Slider hpSlider;

    //HP Text
    public Text hpText;

    //���� HP
    float currentHP = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ĳ���� ��Ʈ�ѷ� ������Ʈ �ҷ�����
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //1. ������� �Է��� ���� (w,s,a,d)
        float h = Input.GetAxis("Horizontal"); // a: -1, d: 1 ������ ������ 0
        float v = Input.GetAxis("Vertical"); // s: -1, w: 1 ������ ������ 0

        //2. ������ ����
        Vector3 dirH = transform.right * h;
        Vector3 dirV = transform.forward * v;
        Vector3 dir = dirH + dirV;

        //���࿡ �����̽��ٸ� ������
        if (Input.GetButtonDown("Jump"))
        {
            //yVelocity�� jumpPower �Ѵ�
            yVelocity = jumpPower;
        }
        //yVelocity ���� ���� �ٿ��ش�. (gravity�� ���ؼ�)
        yVelocity += gravity * Time.deltaTime;

        //dir�� y���� yVelocity�� �����Ѵ�
        dir.y = yVelocity;

        //3. �� �������� �̵�
        //p = p0 + vt
        //transform.position = transform.position + dir * speed * Time.deltaTime;

        cc.Move(dir * speed * Time.deltaTime);
    }

    public void OnDamaged()
    {
        //���� HP�� ���δ�
        currentHP -= 10;
        //HP UI�� �����Ѵ�
        //1. text ����
        hpText.text = "HP : " + currentHP;

        //2. slider ����
        float ratio = currentHP / 100;
        hpSlider.value = ratio;
    }
}
