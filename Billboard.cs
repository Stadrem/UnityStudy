using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //자신의 앞 방향을 카메라의 앞방향과 같게 한다.
        transform.forward = Camera.main.transform.forward;
    }
}
