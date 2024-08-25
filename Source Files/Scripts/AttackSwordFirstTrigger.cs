using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSwordFirstTrigger : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("SwordAttack"))
        {
            Destroy(this.gameObject);
        }
    }
}
