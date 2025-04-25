using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveRigidbody : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody _rigidbody;
    [SerializeField] private InputManager _inputManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        Vector2 move = _inputManager.moveVector;

        // Vector3 direction = new Vector3(move.x, 0, move.y);
        Vector3 movement = new Vector3(move.x, 0, move.y) * speed * Time.deltaTime;
        _rigidbody.MovePosition(_rigidbody.position + movement);
    }
}
