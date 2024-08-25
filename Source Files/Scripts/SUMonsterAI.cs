using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Timers;

[SerializeField]
public class SUMonsterAI : EnemyAI
{
    private double distToPlayer;


    private int throwRocksCooldown = 6000;
    private int slamGroundCooldown = 6000;
    private int chargeCooldown = 8000;
    private int meleeAttackCooldown = 3500;

    public bool isThrowOnCooldown = false;
    public bool isSlamGroundOnCooldown = false;
    public bool isChargeOnCooldown = false;
    public bool isMeleeAttackOnCooldown = false;
    public Vector3 vectorTowardsPlayer;

    public bool isCharging;
    public bool isThrowing;
    public bool isMeleeAttacking;

    public Vector3 playerTransformLocation;

    public GameObject SUMonsterRock;
    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        
        if (animator.GetBool("alive") == true)
        {
            vectorTowardsPlayer = new Vector3(player.transform.position.x - enemy.transform.position.x, player.transform.position.y - enemy.transform.position.y, 0);
            distToPlayer = FindRadius(player.transform.position.x - enemy.transform.position.x, player.transform.position.y - enemy.transform.position.y);
            if (distToPlayer <= 0.5 && !isMeleeAttackOnCooldown)
            {
                MeleeAttack();
            }
            else if (distToPlayer > 0.5 && distToPlayer <= 0.8)
            {
               // SlamGround();
            }
            else if (distToPlayer > 0.8 && distToPlayer <= 1.6 && !isThrowing && !isThrowOnCooldown && !isCharging)
            {
                ThrowRocks();
            }
            else if (distToPlayer > 1.6 && distToPlayer <= 2.3 && !isCharging && !isChargeOnCooldown && !isThrowing)
            {
                playerTransformLocation = player.transform.position;
                Charge();
            }
            else if (!isCharging && !isThrowing && !isMeleeAttacking)
            {
                followPlayer();
            }

        }
        else
        {
            animator.SetBool("isMoving", false);
        }
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
            //to flip sprite
            if (gameObject.transform.position.x > player.transform.position.x)
            {
                Vector3 scalePlaceHolder = gameObject.transform.localScale;
                if (scalePlaceHolder.x > 0)
                {
                    scalePlaceHolder.x *= -1;
                }
                gameObject.transform.localScale = scalePlaceHolder;
            }
            else
            {
                Vector3 scalePlaceHolder = gameObject.transform.localScale;
                scalePlaceHolder.x = Mathf.Abs(scalePlaceHolder.x);
                gameObject.transform.localScale = scalePlaceHolder;
            }
            populateIntMap(x_diff, y_diff, r, isObstructed);
            populateAvoidMap();

            enemy_path = weighTheMaps(x_diff, y_diff, r);
            if (distToPlayer > 0.1)
            {
                enemy.MovePosition(enemy.transform.position + enemy_path * movespeed * Time.fixedDeltaTime);
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
                MeleeAttack();
            }

        }
        else if (r <= 4 && Physics2D.Raycast(transform.position, dirVector, (float)r, LayerMask.GetMask("Obstacles")))
        {
            if (lastKnown.x == enemy.transform.position.x && lastKnown.y == enemy.transform.position.y)
            {
                return;
            }
            // to flip sprite
            if ((lastKnown.x - enemy.transform.position.x) < 0)
            {
                Vector3 scalePlaceHolder = gameObject.transform.localScale;
                if (scalePlaceHolder.x > 0)
                {
                    scalePlaceHolder.x *= -1;
                }
                gameObject.transform.localScale = scalePlaceHolder;
            }
            else
            {
                Vector3 scalePlaceHolder = gameObject.transform.localScale;
                scalePlaceHolder.x = Mathf.Abs(scalePlaceHolder.x);
                gameObject.transform.localScale = scalePlaceHolder;
            }
            double newR = FindRadius((lastKnown.x - enemy.transform.position.x), (lastKnown.y - enemy.transform.position.y));
            populateIntMap((lastKnown.x - enemy.transform.position.x), (lastKnown.y - enemy.transform.position.y), newR, isObstructed);
            populateAvoidMap();

            enemy_path = weighTheMaps((lastKnown.x - enemy.transform.position.x), (lastKnown.y - enemy.transform.position.y), newR);
            if (distToPlayer > 0.1)
            {
                animator.SetBool("isMoving", true);
                enemy.MovePosition(enemy.transform.position + enemy_path * movespeed * Time.fixedDeltaTime);
            }
            else
            {
                animator.SetBool("isMoving", false);
                MeleeAttack();
            }
        }
    }

    private async void MeleeAttack()
    {
        isMeleeAttacking = true;
        isMeleeAttackOnCooldown = true;
        animator.SetBool("isMoving", false);
        animator.SetTrigger("isAttacking");
        await Task.Delay(meleeAttackCooldown);
        isMeleeAttackOnCooldown = false;
        isMeleeAttacking = false;
    }

    private async void ThrowRocks()
    {
        isThrowing = true;
        isThrowOnCooldown = true;
        animator.SetBool("isThrowing", true);
        await Task.Delay(throwRocksCooldown);
        isThrowOnCooldown = false;
    }

    public void StopThrowing()
    {
        animator.SetBool("isThrowing", false);
    }
    private void Throw()
    {
        Instantiate(SUMonsterRock, enemy.transform.position, Quaternion.identity);
    }

    private void Charge()
    {
        isChargeOnCooldown = true;
        isCharging = true;
        animator.SetBool("isCharging", true);
    }

    private async void ChargeTowardsPlayer()
    {
        Vector3 chargeDir = new Vector3(player.transform.position.x - enemy.transform.position.x, player.transform.position.y - enemy.transform.position.y, 0).normalized * 0.2f;
        float elapsedTime = 0f;
        while (Vector3.Distance(enemy.transform.position, playerTransformLocation) >= 0.08f)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 1.5f)
            {
                break;
            }
            RaycastHit2D hit = Physics2D.Raycast(transform.position, chargeDir, 0.2f, LayerMask.GetMask("Obstacles"));
            if (hit.collider != null)
            {
                // Obstacle detected, break the loop
                break;
            }
            enemy.MovePosition(transform.position + chargeDir * 8f * Time.fixedDeltaTime);
            await Task.Yield();
        }
        await Task.Delay(chargeCooldown);
        isChargeOnCooldown = false;
    }

    private void StopCharging()
    {
        movespeed = 0.5f;
        animator.SetBool("isCharging", false);
        isCharging = false;
    }
}
