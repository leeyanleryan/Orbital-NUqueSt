using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAI : EnemyAI
{
    public bool isAttacking;
    public override void Start()
    {
        base.Start();
        isAttacking = false;
    }
    public override void Update()
    {
        if (animator.GetBool("alive") && !isAttacking)
        {
            followPlayer();
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}
