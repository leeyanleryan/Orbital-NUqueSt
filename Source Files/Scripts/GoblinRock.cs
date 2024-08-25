using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;
using System;

public class GoblinRock : MonoBehaviour
{
    public GameObject rock;
    public Transform rockPos;
    private GameObject enemy;
    private GameObject player;
    private Animator enemyAnimator;
    public bool moving;



    private float timer;
    public float force;

    private void Start()
    {
        enemy = gameObject;
        player = GameObject.Find("Player");
        enemyAnimator = gameObject.GetComponent<Animator>();
    }

    private float FindRadius(float x, float y)
    {
        return Mathf.Sqrt((x * x) + (y * y));
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3)
        {
            timer = 0;
            enemyAnimator.SetBool("isMoving", false);
            initiateThrow();
        }
    }

    private void initiateThrow()
    {
        float distToPlayer = FindRadius(player.transform.position.x - enemy.transform.position.x, player.transform.position.y - enemy.transform.position.y);

        if (distToPlayer <= 1.7 || distToPlayer >= 3.5)
        {
            //enemyAnimator.SetBool("isMoving", false);
            enemyAnimator.SetTrigger("attack");
        }
    }

    private void Throw()
    {
        Instantiate(rock, enemy.transform.position, Quaternion.identity);
    } 
}
