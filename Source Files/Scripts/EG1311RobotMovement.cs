using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EG1311RobotMovement : RobotMovement
{
    public GameObject rock;
    public Vector3 directionToThrow;
    public rockTrajectory eg1311rockTrajectory;

    public override void Start()
    {
        base.Start();
        directionToThrow = Vector3.zero;
        eg1311rockTrajectory = rock.GetComponent<rockTrajectory>();
    }
    public override void Update ()
    {
        base.Update();
        if (spriteRenderer.sprite == downFacingSprite)
        {
            directionToThrow = new Vector3(0, -0.2f, 0);
        }
        else if (spriteRenderer.sprite == upFacingSprite)
        {
            directionToThrow = new Vector3(0, 0.2f, 0);
        }
        else if (spriteRenderer.sprite == sideFacingSprite && spriteRenderer.flipX)
        {
            directionToThrow = new Vector3(-0.2f, 0, 0).normalized;
        }
        else if (spriteRenderer.sprite == sideFacingSprite && !spriteRenderer.flipX)
        {
            directionToThrow = new Vector3(0.2f, 0, 0).normalized;
        }
        Shoot();
    }
    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(rock, gameObject.transform.position, Quaternion.identity);
        }
    }
}
