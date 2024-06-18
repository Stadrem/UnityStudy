using UnityEngine;

//화면의 아래로 이동
//플레이어를 추적

public class EnemyMove : MonoBehaviour
{
    //속도
    public float speed = 5f;

    //방향
    Vector3 dir;

    //이펙트 공장
    public GameObject explosionFactory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //랜덤하게 숫자를 정함
        int randomNum = Random.Range(0, 10);
        print(randomNum);

        if(randomNum > 3 ) //30% 확률
        {
            //플레이어 오브젝트 찾기
            GameObject player = GameObject.Find("Player");

            //플레이어의 방향을 계산
            dir = player.transform.position - transform.position;
            dir.Normalize();
        }
        else
        {
            //단순히 아래로 내려가게
            dir = Vector3.down;
        }


    }

    // Update is called once per frame
    void Update()
    {
        //플레이어 추격
        transform.position += dir * speed * Time.deltaTime;
    }

    //충돌시에 Bullet 삭제

    private void OnCollisionEnter(Collision collision)
    {
        //오브젝트가 부딪히면
        GameObject explosion = Instantiate(explosionFactory);
        explosion.transform.position += transform.position;

        //부딪힌 오브젝트가 Bullet이라는 오브젝트라면
        if(collision.gameObject.GetComponent<Bullet>())
        {
            //비활성화
            collision.gameObject.SetActive(false);
        }
        Destroy(gameObject); //나 자신도 삭제
    }


}
