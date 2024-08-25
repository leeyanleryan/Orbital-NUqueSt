using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG2111ARobot : RobotMovement
{
    public RectTransform gpsTracker;
    private float scaleX;
    private float scaleY;

    public override void Start()
    {
        base.Start();
        scaleX = 368.8f / (-0.43f - (-1.97f));
        scaleY = 336.2f / (1.08f - (-0.43f));
    }

    public override void Update()
    {
        Vector2 tempVect = new Vector2();
        tempVect.x = scaleX * (gameObject.transform.position.x + 1.97f);
        tempVect.y = scaleY * (gameObject.transform.position.y + 0.43f);
        gpsTracker.anchoredPosition = tempVect;

        //executed if there is a player keyboard input, the subsequent ifs are to slide along an obstacle
        if (movementInput != Vector2.zero)
        {
            //try movement using the player's both x and y inputs
            bool success = TryMove(movementInput);
            if (!success && movementInput.x != 0)
            {
                //try movement using the player's movement only in the x direction;
                success = TryMove(new Vector2(movementInput.x, 0));
            }
            if (!success && movementInput.y != 0)
            {
                //try movement using the player's movement only in the x direction;
                success = TryMove(new Vector2(0, movementInput.y));
            }
        }
        else
        {
        }

        //Set the direction sprite faces based on movement direction
        if (movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (movementInput.x > 0)
        {

            spriteRenderer.flipX = false;

        }
        else
        {

        }
        if (movementInput.y > 0 && movementInput.x == 0)
        {
            spriteRenderer.sprite = upFacingSprite;
        }
        //to move down animation
        else if (movementInput.y < 0 && movementInput.x == 0)
        {
            spriteRenderer.sprite = downFacingSprite;
        }
        //to move side animation
        else if (movementInput.x != 0 && movementInput.y == 0)
        {
            spriteRenderer.sprite = sideFacingSprite;
        }
    }
}
