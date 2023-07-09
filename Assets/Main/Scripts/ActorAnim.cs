using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActorAnim : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] NavMeshAgent agent;
    bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (!isAttacking) {
            // if (knightRB.velocity == Vector3.zero) {
            if (agent.velocity == Vector3.zero) {
                //animator.GetFloat("Blend");
                //animator.SetFloat("Blend", 0.5f);
                animator.CrossFade("Idle", 0, 0);
            } else {
                //animator.SetFloat("Blend", 1.0f);
                animator.CrossFade("Run", 0, 0);
            }
        }
    }

    public IEnumerator Attack(float cd) {
        isAttacking = true;
        //animator.SetFloat("Blend", 0.0f);
        animator.CrossFade("Attack", 0, 0);
        yield return new WaitForSeconds(cd);
        isAttacking = false;
        //animator.SetFloat("Blend", 0.5f);
        animator.CrossFade("Idle", 0, 0);

    }
}
