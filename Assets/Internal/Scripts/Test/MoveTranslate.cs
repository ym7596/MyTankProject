using UnityEngine;

public class MoveTranslate : MonoBehaviour
{
    public float speed = 3f;

    [SerializeField] private InputManager _inputManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = _inputManager.moveVector;
        //(0,1)
        Vector3 direction = new Vector3(move.x, 0, move.y);
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
