using UnityEngine;
using System.Collections;

/// <summary>
/// キャラクタに何か行動させるスクリプト
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class CharacterAction : MonoBehaviour
{
    //Inspectorからいじれたり見れたりさせる奴
    [SerializeField]
    private float movingTurnSpeed = 360.0f;
    [SerializeField]
    private float stationaryTurnSpeed = 180.0f;
    [SerializeField]
    private float jumpPower = 12.0f;
    [Range(1f, 4f)]
    [SerializeField]
    private float gravity = 2.0f;
    [SerializeField]
    private float runCycleLegOffset = 0.2f;
    [SerializeField]
    private float moveSpeed = 1.0f;
    [SerializeField]
    private float animSpeed = 1.0f;
    [SerializeField]
    private float groundCheckdistance = 0.1f;

    //それ以外
    private Rigidbody charRigidbody;
    private Animator charAnimator;
    private float charTurnAmount;
    private float charForwardAmount;
    private CapsuleCollider charCapsuleCollider;
    private float capsuleHeight;
    private Vector3 capsuleCenter;
    private Vector3 groundNormalVector3;
    private bool isGround;
    private readonly float k_Half = 0.5f;
    private float defualutGroundCheckdistance;

    void Start()
    {
        charAnimator = GetComponent<Animator>();
        charRigidbody = GetComponent<Rigidbody>();
        charCapsuleCollider = GetComponent<CapsuleCollider>();
        capsuleHeight = charCapsuleCollider.height;
        capsuleCenter = charCapsuleCollider.center;

        charRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        defualutGroundCheckdistance = groundCheckdistance;
    }

    /// <summary>
    /// アタッチしたモデルを移動させるメソッド
    /// </summary>
    /// <param name="moveVector3"></param>
    /// <param name="isJump"></param>
    public void Move(Vector3 moveVector3, bool isJump)
    {
        if (moveVector3.magnitude > 1.0f) moveVector3.Normalize();
        moveVector3 = transform.InverseTransformDirection(moveVector3);
        CheckGroundStatus();
        moveVector3 = Vector3.ProjectOnPlane(moveVector3, groundNormalVector3);
        charTurnAmount = Mathf.Atan2(moveVector3.x, moveVector3.z);
        charForwardAmount = moveVector3.z;
        TurnRotation();
        if (isGround)
        {
            CharacterJump(isJump);
        }
        else
        {
            AirborneMovement();
        }

        ScaleCapsule();
        UpdateAnimator(moveVector3);
    }

    void ScaleCapsule()
    {
        if (isGround)
        {
            charCapsuleCollider.height = capsuleHeight / 2.0f;
            charCapsuleCollider.center = capsuleCenter / 2.0f;
        }
        else
        {
            Ray crouchRay = new Ray(charRigidbody.position + Vector3.up*charCapsuleCollider.radius*k_Half, Vector3.up);
            float crouchRayLength = capsuleHeight - charCapsuleCollider.radius*k_Half;
            if (Physics.SphereCast(crouchRay, charCapsuleCollider.radius*k_Half, crouchRayLength))
            {
                return;
            }
            charCapsuleCollider.height = capsuleHeight;
            charCapsuleCollider.center = capsuleCenter;
        }
    }

    public void Attack(bool isAttackTrigger)
    {
        charAnimator.SetBool("isAttackTrigger", isAttackTrigger);
    }

    /// <summary>
    /// 落下時のあれ？（サンプルから引っ張ってきたからわからん）
    /// </summary>
    void AirborneMovement()
    {
        Vector3 extraGravityForce = (Physics.gravity * gravity) - Physics.gravity;
        charRigidbody.AddForce(extraGravityForce);

        groundCheckdistance = charRigidbody.velocity.y < 0 ? defualutGroundCheckdistance : 0.01f;
    }

    /// <summary>
    /// アニメーションを更新する？（サンプルから(ry
    /// </summary>
    /// <param name="moveVector3"></param>
    void UpdateAnimator(Vector3 moveVector3)
    {
        // update the animator parameters
        charAnimator.SetFloat("Forward", charForwardAmount, 0.1f, Time.deltaTime);
        charAnimator.SetFloat("Turn", charTurnAmount, 0.1f, Time.deltaTime);
        charAnimator.SetBool("OnGround", isGround);
        if (!isGround)
        {
            charAnimator.SetFloat("Jump", charRigidbody.velocity.y);
        }

        // calculate which leg is behind, so as to leave that leg trailing in the jump animation
        // (This code is reliant on the specific run cycle offset in our animations,
        // and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
        float runCycle =
            Mathf.Repeat(
                charAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime + runCycleLegOffset, 1);
        float jumpLeg = (runCycle < k_Half ? 1 : -1) * charForwardAmount;
        if (isGround)
        {
            charAnimator.SetFloat("JumpLeg", jumpLeg);
        }

        // the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
        // which affects the movement speed because of the root motion.
        if (isGround && moveVector3.magnitude > 0)
        {
            charAnimator.speed = animSpeed;
        }
        else
        {
            // don't use that while airborne
            charAnimator.speed = 1;
        }
    }

    /// <summary>
    /// アタッチしたモデルをジャンプさせる
    /// </summary>
    /// <param name="isJump"></param>
    private void CharacterJump(bool isJump)
    {
        //ジャンプフラグとアニメーションのステート名が条件に合うか
        if (isJump && charAnimator.GetCurrentAnimatorStateInfo(0).IsName("Ground"))
        {
            charRigidbody.velocity = new Vector3(charRigidbody.velocity.x, jumpPower, charRigidbody.velocity.z);
            isGround = false;
            charAnimator.applyRootMotion = false;
            groundCheckdistance = 0.1f;
        }
    }

    /// <summary>
    /// 地面の状態を確認するメソッド
    /// </summary>
    private void CheckGroundStatus()
    {
        RaycastHit isHit;
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out isHit, groundCheckdistance))
        {
            groundNormalVector3 = isHit.normal;
            isGround = true;
            charAnimator.applyRootMotion = true;
        }
        else
        {
            isGround = false;
            groundNormalVector3 = Vector3.up;
            charAnimator.applyRootMotion = false;
        }
    }

    /// <summary>
    /// アニメーションで移動させるときに呼ばれる？？
    /// </summary>
    public void OnAnimatorMove()
    {
        // we implement this function to override the default root motion.
        // this allows us to modify the positional speed before it's applied.
        if (isGround && Time.deltaTime > 0)
        {
            Vector3 v = (charAnimator.deltaPosition * moveSpeed) / Time.deltaTime;

            // we preserve the existing y part of the current velocity.
            v.y = charRigidbody.velocity.y;
            charRigidbody.velocity = v;
        }
    }

    /// <summary>
    /// アタッチしたモデルを移動ベクトル方向に回転させる
    /// </summary>
    private void TurnRotation()
    {
        var turnSpeed = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, charForwardAmount);
        transform.Rotate(0, charTurnAmount * turnSpeed * Time.deltaTime, 0);
    }
}