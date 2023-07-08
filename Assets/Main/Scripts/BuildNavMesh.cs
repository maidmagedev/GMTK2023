using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class BuildNavMesh : MonoBehaviour
{
    [SerializeField] NavMeshSurface surface;

    void Update() {
        surface.BuildNavMesh();
    }
}
