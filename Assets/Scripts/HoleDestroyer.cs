using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoleDestroyer : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cointag"))
        {
            Destroy(collision.gameObject);
            
        }
        
    }
   
}
