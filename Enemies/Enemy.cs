using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public bool DisableAI = false;
    public float Speed;
    public GameObject PointsObj;

    public NavMeshAgent NavAgent { get; private set; }
    public List<Transform> Points { get; private set; } = new List<Transform>();
    public Vector3 CurrentTargetPos { get; private set; }
    public bool IsVisibleToPlayer { get; private set; } = false;
    public virtual int VisibilityLayerMasks { get; set; }

    private float counterVisibleCheck = 0.0f;
    private void checkVisibility()
    {
        counterVisibleCheck += Time.deltaTime;
        if (counterVisibleCheck >= 0.5f)
        {
            IsVisibleToPlayer = GetComponent<Renderer>().IsVisibleFrom(XRObjectsGet.XRPlayer.Camera, VisibilityLayerMasks);
            counterVisibleCheck = 0.0f;
        }
    }
    public Vector3 GetRandomPointPos()
    {
        int randIndex = RandomNumGen.Instance.Next(0, Points.Count);
        if (Points[randIndex].position.Equals(CurrentTargetPos))
        {
            return GetRandomPointPos();
        }
        return Points[randIndex].position;
    }

    public void SetNewDestination(Vector3 pos)
    {
        CurrentTargetPos = pos;
        NavAgent.SetDestination(CurrentTargetPos);
    }

    // DoThink runs every half a second, DoIdle runs every 30 seconds
    private float counterDoThink = 0.0f;
    private float counterDoIdle = 0.0f;
    public bool Idle { get; set; } = true;
    public virtual void DoThink() { }
    public virtual void DoIdle()
    {
        SetNewDestination(GetRandomPointPos());
    }
    void Update()
    {
        if (!DisableAI)
        {
            checkVisibility();

            counterDoThink += Time.deltaTime;
            if (counterDoThink >= 0.5f)
            {
                counterDoThink = 0.0f;
                DoThink();
            }

            if(Idle)
            {
                counterDoIdle += Time.deltaTime;
                if (counterDoIdle >= 30.0f)
                {
                    counterDoIdle = 0.0f;
                    DoIdle();
                }
            }
        }
    }
    void Awake()
    {
        foreach (Transform child in PointsObj.transform)
        {
            Points.Add(child);
        }

        VisibilityLayerMasks = 1 << LayerMask.NameToLayer("Wall"); // |
        NavAgent = GetComponent<NavMeshAgent>();
        NavAgent.speed = Speed;

        if (!DisableAI)
            SetNewDestination(GetRandomPointPos());
    }
}
