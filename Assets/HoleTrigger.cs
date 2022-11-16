using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleTrigger : MonoBehaviour
{
    [SerializeField] GameObject winPanel;
    [SerializeField] AudioSource SFX;
    bool isGoal = false;

    public bool IsGoal { get => isGoal; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Goal");
            SFX.Play();
            isGoal = true;
        }
    }
}
