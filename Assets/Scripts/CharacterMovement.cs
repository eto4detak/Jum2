using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterMovement : MonoBehaviour
{
    private bool isJumpTarget;
    private bool isJump;
    private Vector3 jumpTarget;
    private Vector3 moveDirection;
    private Vector3 gravityCenter = Vector3.zero;
    private int planetMask;
    private float maxJumpDistance = 20f;
    private float jumpForce = 6f;
    private float jumpTime = 1f;
    private float planetRadius = 100f;
    private TrailRenderer trail;
    private void Awake()
    {
        trail = GetComponent<TrailRenderer>();
        jumpTarget = transform.position;
        planetMask = LayerMask.GetMask("Planet");
    }

    private void Update()
    {
        if (isJump) return;
        if (isJumpTarget)
        {
            JumpToTarget();
        }
        else
        {
            JumpForvard();
        }
    }


    public void UpLevel(Material mat)
    {
        jumpTime = jumpTime / 1.3f;
        trail.material = mat;
    }


    public void SetTargetPosition(Vector3 target)
    {
        isJumpTarget = true;
        jumpTarget = target;
    }

    private void JumpToTarget()
    {
        moveDirection = jumpTarget - transform.position;
        Jump();
    }

    private void JumpForvard()
    {
        moveDirection = transform.forward * maxJumpDistance;
        Jump();
    }

    private void Jump()
    {
        isJumpTarget = false;
        isJump = true;
        if (moveDirection.magnitude >= maxJumpDistance)
        {
            moveDirection = moveDirection.normalized * maxJumpDistance;
            jumpTarget = (transform.position + moveDirection).normalized * planetRadius;
        }
        transform.DOLookAt(jumpTarget, 0, AxisConstraint.None, transform.position);
        transform.DOJumpOne(jumpTarget, jumpForce, jumpTime, transform.position)
            .OnComplete(() =>
            {
                isJump = false;
            }
            );
    }

}
