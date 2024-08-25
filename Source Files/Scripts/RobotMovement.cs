using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
[SerializeField]
public class RobotMovement : MonoBehaviour
{
    [SerializeField]
    public Vector2 movementInput;
    public SpriteRenderer spriteRenderer;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>(); //to check for collision
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;
    private Rigidbody2D rb;
    private float movespeed = 0.3f;

    public Sprite sideFacingSprite;
    public Sprite downFacingSprite;
    public Sprite upFacingSprite;
    // Start is called before the first frame update
    public virtual void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
                                        
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    protected bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            movespeed * Time.fixedDeltaTime + collisionOffset);
            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * movespeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public virtual void Update()
    {
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
