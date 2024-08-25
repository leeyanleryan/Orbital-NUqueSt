using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potholes : MonoBehaviour
{
    public Rigidbody2D robotRB;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("robot"))
        {
            robotRB.transform.position = new Vector3(-1.933f, -0.352f, 0);
            // play dropping down animation

        }
    }
}
