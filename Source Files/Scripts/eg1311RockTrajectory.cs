using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class eg1311RockTrajectory : rockTrajectory
{
    public EG1311RobotMovement eg1311RobotMovement;
    public override void Start()
    {
        eg1311RobotMovement = GameObject.Find("Robot").GetComponent<EG1311RobotMovement>();
        rb = GetComponent<Rigidbody2D>();
        Vector3 direction = eg1311RobotMovement.directionToThrow;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
    }
}
