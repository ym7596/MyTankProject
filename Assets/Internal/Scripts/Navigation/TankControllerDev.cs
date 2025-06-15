using UnityEngine;

public class TankControllerDev : BaseNav
{
    [SerializeField] private Transform _targetPos;

    public GameObject projectilePrefab;
    public Transform firePoint;

    public Transform _target;

    public Transform rootTop;

    public float rotSpeed = 5f;
    public float fireSpeed =  25f;


    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _agent.SetDestination(_targetPos.position);
    }

    private void Update()
    {
        if (_target != null)
        {
            RotateTowardTarget();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject missile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Vector3 dir = firePoint.forward;
            Vector3 velocity = dir * fireSpeed;
            MissileManager.Instance.AddMissile(missile, firePoint.position, velocity);
        }
    }

    private void RotateTowardTarget()
    {
        Vector3 curDirection = rootTop.forward;

        Vector3 targetDirection = (_target.position - rootTop.position).normalized;

        targetDirection.y = 0;

        Quaternion currentRotation = rootTop.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        targetRotation = Quaternion.Euler(-45f, targetRotation.eulerAngles.y, 0f);

        rootTop.rotation = Quaternion.RotateTowards(currentRotation, targetRotation, rotSpeed * Time.deltaTime);
    }
}
