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
        //�ڽ��� �� ������ ī�޶��� �չ���� ���� �Ѵ�.
        transform.forward = Camera.main.transform.forward;
    }
}