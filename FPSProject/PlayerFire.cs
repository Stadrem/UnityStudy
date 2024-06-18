using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    //Bomb 공장(프리팹)
    public GameObject bombFactory;

    //발사 위치
    public GameObject firePos;

    //발사 힘
    public float firePower = 100;

    //파편 효과 공장(프리팹)
    public GameObject bulletImpactFactory;

    float myTime = 0f; // 초기화
    float nextFire = 0.25f; // 0.1초 딜레이

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //1. 마우스 오른쪽 버튼을 누르면
        if (Input.GetButtonDown("Fire2"))
        {
            //2. Bomb 공장에서 Bomb을 하나 생성
            GameObject bomb = Instantiate(bombFactory);

            //3. 생성된 Bomb의 위치를 firePos에 위치
            bomb.transform.position = firePos.transform.position;

            //4. 생성된 Bomb이 카메라의 앞방향으로 나아갈 수 있게 힘을 준다.
            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            rb.AddForce(Camera.main.transform.forward * firePower);
        }

        myTime += Time.deltaTime; // 시간 업데이트

        //1. 마우스 왼쪽 버튼을 누르면
        if (Input.GetButton("Fire1") && myTime > nextFire)
        {
            // myTime을 초기화하여 딜레이를 재설정
            myTime = 0f;

            //2. 카메라 위치, 카메라 앞방향 Ray를 만듬.
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            //3. Ray를 발사해서 어딘가에 맞았다면
            RaycastHit hitInfo = new RaycastHit();

            if(Physics.Raycast(ray, out hitInfo))
            {
                //4. 맞은 위치에 파편효과를 보여준다.
                //Debug.Log("맞은 오브젝트 : " + hitInfo.transform.name);
                //4-1 파편 효과 공장에서 파편 효과 만들기
                GameObject bulletImpact = Instantiate(bulletImpactFactory);

                //4-2 만든 파편 효과를 맞은 위치에 생성
                bulletImpact.transform.position = hitInfo.point;

                //4-3 만든 파편 효과를 앞 방향을 맞은 위치의 Normal 값으로 셋팅
                bulletImpact.transform.forward = hitInfo.normal;

                //4-4 만든 파편 효과를 2초 뒤에 파괴
                Destroy(bulletImpact, 1);

                //5. 맞은 대상이 Enemy
                if (hitInfo.transform.gameObject.name.Contains("Enemy"))
                {
                    //6. Enemy에게 Damage 가하기
                    //6-1. Enemy에서 Enemy 컴포넌트를 가져오기
                    Enemy enemy = hitInfo.transform.GetComponent<Enemy>();

                    //6-2. 가져온 컴포넌트에서 OnDamaged 함수를 호출
                    enemy.OnDamaged();
                }
            }
        }
    }
}
