using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorDetectOtherActors : MonoBehaviour
{
    [SerializeField] ActorInfo.Team detectTeam; // only detect actors from this team.
    [SerializeField] ActorBrain myActorBrain;
    void OnTriggerEnter(Collider other) {

        // Is this object NOT an actor?
        if (!other.gameObject.TryGetComponent<ActorInfo>(out ActorInfo actor)) {
            return;
        }
        // This actor is myself. Exclude.
        if (actor == myActorBrain.myActorInfo) {
            return;
        }
        // This actor is from a team we don't care about.
        if (actor.team != detectTeam) {
            return;
        }
        myActorBrain.EnemiesInRange.Add(actor);
    }

    void OnTriggerExit(Collider other) {
        // Is this object NOT an actor?
        if (!other.gameObject.TryGetComponent<ActorInfo>(out ActorInfo actor)) {
            return;
        }
        // This actor is myself. Exclude.
        if (actor == myActorBrain.myActorInfo) {
            return;
        }
        // This actor is from a team we don't care about.
        if (actor.team != detectTeam) {
            return;
        }
        myActorBrain.EnemiesInRange.Remove(actor);
    }

}
