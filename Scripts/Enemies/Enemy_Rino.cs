using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Rino : Enemy
{
    [Header("Rino Details")]
    [SerializeField] private Vector2 impactPower;
   

    protected override void Update()
    {
        base.Update();

        if (isDead)
            return;

        if (isPlayerDetected && isGrounded)
            canMove = true;

        HandleCharge();
    }

    private void HandleCharge()
    {
        if (!canMove)
            return;

        rb.linearVelocity = new Vector2(moveSpeed * facingDir, rb.linearVelocity.y);

        if (isWallDetected)
            WallHit();

        if (!isGroundInfrontDetected)
            TurnAround();

    }

    private void WallHit()
    {
        anim.SetTrigger("hitWall");
        canMove = false;
        rb.linearVelocity = new Vector2(impactPower.x * - facingDir, impactPower.y);
        Invoke(nameof(Flip), 1f);
    }

    private void TurnAround()
    {
        Flip();
        canMove = false;
        rb.linearVelocity = Vector2.zero;
    }
   
}
