using Unity.Entities;
using UnityEngine;

public struct Movement : IComponentData
{ 
    public float speed;
    public float direction;
}
