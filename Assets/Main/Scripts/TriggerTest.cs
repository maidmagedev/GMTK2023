using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    [SerializeField] GameObject obj;
    void OnTriggerEnter() {
        Debug.Log("Trigger Enter!");
        obj.SetActive(true);
    }
}
