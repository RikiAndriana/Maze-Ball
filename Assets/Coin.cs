using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] Inventory player;
    [SerializeField] AudioSource SFX;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.coin++;
            SFX.Play();
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        this.transform.Rotate(0, 0, 500 * Time.deltaTime);
    }
}
