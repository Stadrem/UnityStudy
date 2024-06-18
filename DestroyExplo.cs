using UnityEngine;

//2초가 지나면 나 자신을 파괴

public class DestroyExplo : MonoBehaviour
{
    //현재시간
    float currentTime = 0;

    //파괴시간
    float destroyTime = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //1. 현재 시간을 누적
        currentTime += Time.deltaTime;

        //2. 현재 시간이 파괴 시간보다 커지면
        if (currentTime > destroyTime)
        {
            //3. 나를 파괴
            Destroy(gameObject);
        }

    }
}
