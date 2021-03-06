using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHp : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if(controller != null)
        {
            if(controller.currentHp < controller.maxHp)
            {
                controller.ChangeHp(5);
                Destroy(gameObject);
            }
        }
    }
}
