using UnityEngine;

public class RaycastTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            int layerMask = 1 << LayerMask.NameToLayer("Enemy");
            if(Physics.Raycast(ray, out hit, 100f, layerMask))
            {
                Debug.Log("오브젝트는 ? : "+hit.collider.name);
            }
        }
    }
}
