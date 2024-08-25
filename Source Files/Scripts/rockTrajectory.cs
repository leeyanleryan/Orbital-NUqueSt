using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class rockTrajectory : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;

    public float force;
    public float rotationRate;
    public float rot;

    public float timer;
    // Start is called before the first frame update
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2 (direction.x, direction.y).normalized * force;

        rot = Mathf.Atan2(-direction.y , -direction.x) * Mathf.Rad2Deg;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (gameObject != null)
        {
            rot += 0.4f;
            transform.rotation = Quaternion.Euler(0, 0, rot);
        }
        timer += Time.deltaTime;
        if (timer > 6)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Obstacles"))
        {
            Destroy(gameObject);
        }
    }
}
