using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorInfo : MonoBehaviour
{
    public string actorName;
    public int currHealth;
    public int maxHealth;
    public Team team;
    public enum Team {
        hero,
        villain
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecieveDamage(int amount) {
        currHealth -= amount;
        if (currHealth < 0) {
            Die();
        }
    }

    public void RecieveDamage(int amount, ActorInfo sender) {
        currHealth -= amount;
        if (currHealth < 0) {
            Debug.Log(actorName + " has been killed by " + sender.actorName + "!");
            Die();
        }
    }

    public void Die() {
        if (transform != null && transform.parent != null && transform.parent.gameObject != null) {
            Destroy(transform.parent.gameObject);
        }
    }
}
