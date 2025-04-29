using UnityEngine;

public class TankControllerDev : BaseNav
{
    [SerializeField] private Transform _targetPos;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _agent.SetDestination(_targetPos.position);
    }
}
