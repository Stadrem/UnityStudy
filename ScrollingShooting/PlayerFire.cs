using UnityEngine;
//fire1을 클릭 했을 때
//미사일을 발사

public class PlayerFire : MonoBehaviour
{
    //주문서
    public GameObject bulletFactory;

    //미사일의 위치를 지정할 포인트
    public GameObject firePosition;

    //미사일을 담을 수 있는 배열
    public GameObject[] bulletArray;

    //빈칸의 갯수
    public int bulletLength = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //배열을 이용해서 공간을 확보.
        bulletArray = new GameObject[bulletLength];
        //비어있는 배열을 채움 (반복분)
        for(int i = 0; i < bulletLength; i++)
        {
            //총알을 받음
            GameObject go = Instantiate(bulletFactory);

            //총알을 채워넣음
            bulletArray[i] = go;

            //스위치를 끔
            go.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //유저가 Fire1을 클릭하면
        if (Input.GetButtonDown("Fire1"))
        {
            //비활성화 되어있는 Bullet 찾기
            for (int i = 0; i < bulletLength; i++) 
            {
                //bulletArray[i]번째 총알이 비활성화 되어있다면
                if (bulletArray[i].activeSelf == false)
                {
                    //firePosition 위치로 이동
                    bulletArray[i].transform.position = firePosition.transform.position;
                    //활성화
                    bulletArray[i].SetActive(true);
                    //반복문 종료
                    break;
                }
            }
        }
    }
}
