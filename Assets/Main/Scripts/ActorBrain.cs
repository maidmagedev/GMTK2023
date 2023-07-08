using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorBrain : MonoBehaviour
{
    public ActorInfo myActorInfo;
    public BasicAI myAI;
    public ActorAnim myAnimator;
    public ActionState myCurrActionState = ActionState.passive; 
    public ActionState myPriorityState = ActionState.attacking; // When split between reaching a destination, it will instead attack enemies.

    public List<ActorInfo> EnemiesInRange;

    public enum ActionState {
        passive, // PRIORITY: Does nothing.
        attacking, // PRIORITY: Attacks nearby enemies, then will return to original path once clear.
        running // PRIORITY: Ignores any nearby enemies, just beelines towards the desired path.
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemiesInRange.Count > 0) {
            // If I am not attacking and I want to prioritize attacking, then I will start an attack.
            if (myCurrActionState != ActionState.attacking && myPriorityState != ActionState.running) {
                ActorInfo target = GetClosestEnemyInRange(); // GetClosestEnemyInRange performs a cleanup, so it could return null.
                if (target != null) {
                    StartCoroutine(AttackActor(target));
                }
            }
        }
    }

    public void AddToEnemiesInRange(ActorInfo actor) {
        EnemiesInRange.Add(actor);
    }

    public ActorInfo GetClosestEnemyInRange() {
        if (EnemiesInRange.Count <= 0) {
            return null;
        }
        ActorInfo closestActor = EnemiesInRange[0];
        foreach (ActorInfo enemy in EnemiesInRange) {
            if (enemy == null) {
                EnemiesInRange.Remove(enemy);
                return GetClosestEnemyInRange();
            }
            float distanceToEnemy = Vector3.Distance(enemy.transform.position, myActorInfo.transform.position);
            float distanceToLastKnownClosest = Vector3.Distance(closestActor.transform.position, myActorInfo.transform.position);
            if (distanceToEnemy < distanceToLastKnownClosest) {
                closestActor = enemy;
            }
        }
        return closestActor;
    }

    public IEnumerator AttackActor(ActorInfo actor) {
        myCurrActionState = ActionState.attacking;
        Debug.Log(myActorInfo.actorName + " is attacking " + actor.actorName);
        myAI.PauseAI(true);
        if (myAnimator != null) {
            this.transform.LookAt(actor.transform);
            StartCoroutine(myAnimator.Attack());
        }
        // actor.currHealth -= 10;
        yield return new WaitForSeconds(0.667f);
        actor.RecieveDamage(10, myActorInfo);
        yield return new WaitForSeconds(1.0f);
        myAI.PauseAI(false);
        myCurrActionState = ActionState.passive;
    }
}
