using UnityEngine;

public class ERotation : MonoBehaviour
{

    //euler 각  (30도 60도 90도)
    //qauternion 각 x,y,z,w (0,30,60)

    //짐벌락
    // x축이 회전할 때 다른 축 회전 제한이되거나 의도치않게 움직일 수 있다.
    // 0,60,10
    Vector3 eulerAngle = new Vector3(30, 60, 90);
    float yrot = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.eulerAngles = eulerAngle; // euler 각을 사용하여 회전
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion deltaRot = Quaternion.Euler(0, 30f*Time.deltaTime, 0);
        transform.rotation = transform.rotation * deltaRot; // 쿼터니온을 사용하여 회전
    }
}
