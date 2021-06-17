using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAmo : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if(controller != null)
        {
            controller.ChangeAmo(5);
            Destroy(gameObject); 
        }
    }
}
