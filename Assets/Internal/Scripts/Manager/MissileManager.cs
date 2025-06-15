
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class MissileManager : SingleTon<MissileManager>
{
    private List<GameObject> missileObjects = new List<GameObject>();
    private List<Vector3> velocityList = new List<Vector3>();

    private NativeArray<Vector3> positions;
    private NativeArray<Vector3> velocities;

    private float elapsedTime = 0f;

    protected override void Awake()
    {
        base.Awake();
    }

    public void AddMissile(GameObject missile, Vector3 startPosition, Vector3 initialVelocity)
    {
        missileObjects.Add(missile);
        velocityList.Add(initialVelocity);
    }

    public void RemoveMissile(GameObject missile)
    {
        int index = missileObjects.IndexOf(missile);
        if (index != -1)
        {
            missileObjects.RemoveAt(index);
            velocityList.RemoveAt(index);
        }

    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        int count = missileObjects.Count;
        if (count == 0) return;

        float deltaTime = Time.deltaTime;
        elapsedTime += deltaTime;

        if (positions.IsCreated) positions.Dispose();
        if (velocities.IsCreated) velocities.Dispose();

        positions = new NativeArray<Vector3>(count, Allocator.TempJob);
        velocities = new NativeArray<Vector3>(count, Allocator.TempJob);

        for (int i = 0; i<count; i++)
        {
            positions[i] = missileObjects[i].transform.position;
            velocities[i] = velocityList[i];
        }

        MissileJob job = new MissileJob()
        {
            deltaTime = deltaTime,
            gravity = Physics.gravity,
            positions = positions,
            velocities = velocities,
        };

        JobHandle handle = job.Schedule(count, 64);
        handle.Complete();

        for(int i = 0;i<count; i++)
        {
            var missile = missileObjects[i];
            Vector3 prevPos = missile.transform.position;
            Vector3 nextPos = positions[i];

            missile.transform.position = nextPos;

            Vector3 direction = (nextPos -prevPos).normalized;
            if(direction.sqrMagnitude > 0.0001f)
            {
                missile.transform.rotation = Quaternion.LookRotation(direction);
            }

            velocityList[i] = velocities[i];
        }

        positions.Dispose();
        velocities.Dispose();
    }
}


[BurstCompile]
struct MissileJob : IJobParallelFor
{
    public float deltaTime;
    public Vector3 gravity;

    public NativeArray<Vector3> positions;
    public NativeArray<Vector3> velocities;

    public void Execute(int index)
    {
        Vector3 velocity = velocities[index];
        velocity += gravity * deltaTime;
        Vector3 position = positions[index] + velocity * deltaTime;

        positions[index] = position;
        velocities[index] = velocity;
    }
}