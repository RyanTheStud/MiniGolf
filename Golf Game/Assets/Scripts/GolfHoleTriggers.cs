using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfHoleTriggers : MonoBehaviour
{
    [SerializeField] MenuManager menuManager;    

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            menuManager.isWon = true;
        }
    }
}
