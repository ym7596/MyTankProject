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

    public Transform targetPos;



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
