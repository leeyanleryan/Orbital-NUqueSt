using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAI : EnemyAI
{
    public Animator skeletonAnimation;
    public bool isAttacking;
    public bool hasAttacked;
    public float timer;
    public Vector3 posToChase;
    public override void Start()
    {
        
        base.Start();
        timer = 0;
        isAttacking = false;
        hasAttacked = false;
        skeletonAnimation = GetComponent<Animator>();
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        double distToPlayerUpdate = FindRadius(player.transform.position.x - enemy.transform.position.x, player.transform.position.y - enemy.transform.position.y);
        if (timer > 4)
        {
            hasAttacked = false;
        }
        else
        {
            hasAttacked = true;
        }
        if (distToPlayerUpdate <= 0.2 && timer > 4 && !isAttacking && animator.GetBool("alive"))
        {
            timer = 0;
            hasAttacked = true;
            skeletonAnimation.SetTrigger("attack");
            if (gameObject.transform.position.x > player.transform.position.x)
            {
                enemySpriteRenderer.flipX = true;
            }
            else
            {
                enemySpriteRenderer.flipX = false;
            }
        }
        else if (distToPlayerUpdate > 0.2 && distToPlayerUpdate <= 0.7 && !hasAttacked && !isAttacking && animator.GetBool("alive"))
        {
            if (gameObject.transform.position.x > player.transform.position.x)
            {
                enemySpriteRenderer.flipX = true;
            }
            else
            {
                enemySpriteRenderer.flipX = false;
            }
            Vector3 dirToPlayer = new Vector3(player.transform.position.x - enemy.transform.position.x, player.transform.position.y - enemy.transform.position.y, 0);
            LayerMask layersToAvoid = LayerMask.GetMask("Obstacles");
            RaycastHit2D hitObstacle = Physics2D.Raycast(transform.position, dirToPlayer, 0.25f, layersToAvoid);
            if (!hitObstacle.collider)
            {
                enemy.MovePosition(enemy.transform.position + dirToPlayer * movespeed * 2 * Time.fixedDeltaTime);
                skeletonAnimation.SetBool("isMoving", true);
            }
            else
            {
                followPlayer();
            }
            
        }
        else if (animator.GetBool("alive") && !isAttacking)
        {
            followPlayer();
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
    public override Vector3 weighTheMaps(double x_diff, double y_diff, double radius)
    {
        double xToPlayer = player.transform.position.x - enemy.transform.position.x;
        double yToPlayer = player.transform.position.y - enemy.transform.position.y;
        double r = FindRadius(xToPlayer, yToPlayer);
        Vector3 dirToPlayer = new Vector3((float)xToPlayer, (float)yToPlayer, 0);
        bool isObstructed = Physics2D.Raycast(transform.position, dirToPlayer, (float)r, LayerMask.GetMask("Obstacles"));

        Vector3 resultantVector = new Vector3();

        if ((radius > 0.7 || radius < 0.58) || isObstructed)
        {
            for (int x = 0; x < 8; x += 1)
            {
                weightedMap[x] = interestMap[x] - avoidanceMap[x];
                if (weightedMap[x] > 0)
                {
                    resultantVector += (dirArray[x] * (float)weightedMap[x]).normalized * (float)0.20;
                }
            }
            return resultantVector;
        }

        //circling around
        // isObstructed is to see if there is a clear line of sight between enemy and player. Returns true if there is no clear line of sight
        if (radius >= 0.58 && radius <= 0.7 && !isObstructed)
        {
            Vector3 resultantVectorCircle = new Vector3();
            // obstructed is to see if enemy detects a collision while circling
            if (obstructed == false)
            {
                resultantVectorCircle = new Vector3((float)y_diff * -1, (float)x_diff, 0);
                if (Physics2D.Raycast(enemy.position, resultantVectorCircle, 0.08f, LayerMask.GetMask("Obstacles", "enemy")))
                {
                    //print("collision detected");
                    resultantVectorCircle = new Vector3((float)y_diff, (float)x_diff * -1, 0);
                    obstructed = true;
                }
            }
            else if (obstructed == true)
            {
                resultantVectorCircle = new Vector3((float)y_diff, (float)x_diff * -1, 0);
                if (Physics2D.Raycast(enemy.position, resultantVectorCircle, 0.08f, LayerMask.GetMask("Obstacles", "enemy")))
                {
                    //print("collision detected");
                    resultantVectorCircle = new Vector3((float)y_diff * -1, (float)x_diff, 0);
                    obstructed = false;
                }
            }
            return resultantVectorCircle;
        }

        return Vector3.zero;
    }
    public override void followPlayer()
    {
        double x_diff = player.transform.position.x - enemy.transform.position.x;
        double y_diff = player.transform.position.y - enemy.transform.position.y;

        Vector3 dirVector = new Vector3((float)x_diff, (float)y_diff, 0);
        double r = FindRadius(x_diff, y_diff);

        bool isObstructed = Physics2D.Raycast(transform.position, dirVector, (float)r, LayerMask.GetMask("Obstacles"));

        if (r <= 4 && !Physics2D.Raycast(transform.position, dirVector, (float)r, LayerMask.GetMask("Obstacles")))
        {
            updatePlayerPos(player.transform.position.x, player.transform.position.y);
            if (gameObject.transform.position.x > player.transform.position.x)
            {
                enemySpriteRenderer.flipX = true;
            }
            else
            {
                enemySpriteRenderer.flipX = false;
            }
            populateIntMap(x_diff, y_diff, r, isObstructed);
            populateAvoidMap();

            enemy_path = weighTheMaps(x_diff, y_diff, r);
            enemy.MovePosition(enemy.transform.position + enemy_path * movespeed * Time.fixedDeltaTime);
            skeletonAnimation.SetBool("isMoving", true);
        }
        else if (r <= 5 && Physics2D.Raycast(transform.position, dirVector, (float)r, LayerMask.GetMask("Obstacles")))
        {
            if (lastKnown.x == enemy.transform.position.x && lastKnown.y == enemy.transform.position.y)
            {
                return;
            }
            if ((lastKnown.x - enemy.transform.position.x) < 0)
            {
                enemySpriteRenderer.flipX = true;
            }
            else
            {
                enemySpriteRenderer.flipX = false;
            }
            double newR = FindRadius((lastKnown.x - enemy.transform.position.x), (lastKnown.y - enemy.transform.position.y));
            populateIntMap((lastKnown.x - enemy.transform.position.x), (lastKnown.y - enemy.transform.position.y), newR, isObstructed);
            populateAvoidMap();

            enemy_path = weighTheMaps((lastKnown.x - enemy.transform.position.x), (lastKnown.y - enemy.transform.position.y), newR);
            enemy.MovePosition(enemy.transform.position + enemy_path * movespeed * Time.fixedDeltaTime);
            skeletonAnimation.SetBool("isMoving", true);
        }
    }

    public override void populateIntMap(double x_toTarget, double y_toTarget, double r, bool isObstructed)
    {
        double componentOfDiag = Mathf.Sqrt((5 * 5) / 2);
        interestMap[0] = ((0 * x_toTarget) + (5 * y_toTarget)) / (5 * normaliseVector(x_toTarget, y_toTarget));    //North 
        interestMap[1] = ((componentOfDiag * x_toTarget) + (componentOfDiag * y_toTarget)) / (5 * normaliseVector(x_toTarget, y_toTarget));    //North-East 
        interestMap[2] = ((5 * x_toTarget) + (0 * y_toTarget)) / (5 * normaliseVector(x_toTarget, y_toTarget));    //East 
        interestMap[3] = ((componentOfDiag * x_toTarget) + (-componentOfDiag * y_toTarget)) / (5 * normaliseVector(x_toTarget, y_toTarget));    //South-East
        interestMap[4] = ((0 * x_toTarget) + (-5 * y_toTarget)) / (5 * normaliseVector(x_toTarget, y_toTarget));    //South
        interestMap[5] = ((-componentOfDiag * x_toTarget) + (-componentOfDiag * y_toTarget)) / (5 * normaliseVector(x_toTarget, y_toTarget));    //South-West
        interestMap[6] = ((-5 * x_toTarget) + (0 * y_toTarget)) / (5 * normaliseVector(x_toTarget, y_toTarget));    //West
        interestMap[7] = ((-componentOfDiag * x_toTarget) + (componentOfDiag * y_toTarget)) / (5 * normaliseVector(x_toTarget, y_toTarget));    //North-West


        if (r < 0.58 && !isObstructed)
        {
            for (int x = 0; x < 8; x += 1)
            {
                interestMap[x] *= -1;
            }
        }
    }
}
