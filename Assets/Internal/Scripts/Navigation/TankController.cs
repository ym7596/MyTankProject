using UnityEngine;


public enum TankState
{
    None,
    Move,
    Guard,
    Attack,
    Die
}

public class TankController : BaseNav
{

    private TankState state = TankState.None;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _firePosition;

    public Transform targetPos;

    public Vector3 shootDirection = new Vector3(45, 0, 0);

    public float force = 150f;

    public float fireInterval = 2f;

    private float timer = 0f;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        state = TankState.Move;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        switch (state)
        {
            case TankState.None:
                {
                    break;
                }
            case TankState.Move:
                {
                    _agent.SetDestination(targetPos.position);
                    break;
                }
            case TankState.Guard:
                {
                    _agent.isStopped = true;
                    timer += Time.deltaTime;
                    if (timer > fireInterval) 
                    {
                        Fire();
                        timer = 0f;
                    }
                    break;
                }
            case TankState.Attack:
                {
                    break;
                }
            case TankState.Die:
                {
                    break;
                }
            default:
                break;
        }
    }

    private void Fire()
    {
        GameObject proj = Instantiate(_projectile, _firePosition.position, Quaternion.identity);

        Rigidbody rigid = proj.GetComponent<Rigidbody>();

        if(rigid != null)
        {
            Vector3 ShootDir = Quaternion.Euler(shootDirection) * Vector3.forward;

            rigid.AddForce(ShootDir * force);
        }
    }

    public void UpdateTargetPos(Vector3 pos)
    {
        targetPos.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Camp"))
        {
            Debug.Log("Iscamp!!");
            state = TankState.Guard;
        }
    }
}
