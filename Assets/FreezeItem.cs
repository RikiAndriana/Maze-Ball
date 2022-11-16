using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeItem : MonoBehaviour
{
    public bool IsFreeze = false;
    [SerializeField] AudioSource SFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            IsFreeze = true;
            SFX.Play();
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        this.transform.Rotate(0, 200 * Time.deltaTime, 0);
    }
}
