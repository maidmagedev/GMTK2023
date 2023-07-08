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
                animator.SetFloat("Blend", 0.5f);
            } else {
                animator.SetFloat("Blend", 1.0f);
            }
        }
    }

    public IEnumerator Attack() {
        isAttacking = true;
        animator.SetFloat("Blend", 0.0f);
        yield return new WaitForSeconds(1.667f);
        isAttacking = false;
        animator.SetFloat("Blend", 0.5f);
    }
}
