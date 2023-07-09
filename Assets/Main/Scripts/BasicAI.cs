using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicAI : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    public PathfindingState currentPfState;
    [Header("Settings")]
    public float desiredStoppingDistance = 2.5f;

    public enum PathfindingState {
        following, // keep tracking a target as it moves.  
        movingToDestination, // reach the destination, and stop moving once it has been reached.
        stationary // not currently intending to move anywhere.
    }

    void Update() {
        SetPathfindingState(currentPfState); // testing- remove later.
        GoToTarget();
    }

    void SetPathfindingState(PathfindingState state) {
        switch (state) {
            case PathfindingState.following:
                agent.stoppingDistance = desiredStoppingDistance;
                break;
            case PathfindingState.movingToDestination:
                agent.stoppingDistance = 0.0f;
                break;
            case PathfindingState.stationary:
                agent.SetDestination(agent.transform.position);
                break;
        }
        currentPfState = state;
    }

    void GoToTarget() {
        if (target == null) {
            return;
        }
        if (agent.isStopped) {
            return;
        }
        if (currentPfState == PathfindingState.stationary) {
            return;
        }
        Vector3 flatPositionAgent = new Vector3(agent.transform.position.x, 0f, agent.transform.position.z);
        Vector3 flatPositionTarget = new Vector3(target.position.x, 0f, target.position.z);

        // Distance is more lenient on height, but you need to be closer on the flat axis.
        if (Vector3.Distance(flatPositionAgent, flatPositionTarget) < 0.05f && Mathf.Abs(agent.transform.position.y - target.position.y) < 0.5f) {
            ConditionReachedDestination();
        }
        agent.ResetPath();
        agent.SetDestination(target.position);
    }

    void ConditionReachedDestination() {
        if (currentPfState == PathfindingState.movingToDestination) {
            SetPathfindingState(PathfindingState.stationary);

        }
    }

    public void PauseAI(bool pause) {

        if (pause) {
            agent.isStopped = true;
        } else {
            agent.isStopped = false;
        }
    }
}
