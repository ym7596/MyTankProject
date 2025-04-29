using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BaseNav : MonoBehaviour
{
    protected NavMeshAgent _agent;

    protected void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
}
