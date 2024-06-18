using UnityEngine;

//일정 시간이 지나면 에너미를 생산해라

public class EnemySpawn : MonoBehaviour
{
    //에너미 prefab
    public GameObject enemyFactory;

    //일정한 시간
    public float createTime = 2f;
    float currentTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //랜덤하게 생산시간을 2~5초 사이로
        createTime = Random.Range(2f, 5f);
        //현재 시간을 0으로 초기화
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //생산시간보다 현재시간이 작다면
        //시간을 누적시킨다
        if (currentTime < createTime) 
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            //현재 시간이 생산시간보다 같거나 커졌다면
            //enemy를 생산해라
            GameObject go = Instantiate(enemyFactory);
            //생산된 에너미의 위치를 나의 위치로 이동
            go.transform.position = transform.position;
            //랜덤하게 생산시간을 2~5초 사이로
            createTime = Random.Range(2f, 5f);
            //현재 시간을 0으로 초기화
            currentTime = 0;
        }
    }
}
