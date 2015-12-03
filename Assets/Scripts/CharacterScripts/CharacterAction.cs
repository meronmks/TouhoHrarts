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
    [SerializeField] private float movingTurnSpeed = 360.0f;
    [SerializeField] private float stationaryTurnSpeed = 180.0f;
    [SerializeField] private float jumpPower = 12.0f;
    [Range(1f, 4f)] [SerializeField] private float gravity = 2.0f;
    [SerializeField] private float runCycleLegOffset = 0.2f;
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float animSpeed = 1.0f;
    [SerializeField] private float isGroundCheckdistance = 0.1f;
    [SerializeField] private bool isGround = false;

    //それ以外
    private Rigidbody charRigidbody;
    private Animator charAnimator;
    private CapsuleCollider charCapsuleCollider;
    private float capsuleHeight;
    private Vector3 capsuleCenter;
    private float capsuleRadius;

    void Start ()
    {
        charAnimator = GetComponent<Animator>();
        charRigidbody = GetComponent<Rigidbody>();
        charCapsuleCollider = GetComponent<CapsuleCollider>();
        capsuleHeight = charCapsuleCollider.height;
        capsuleCenter = charCapsuleCollider.center;
        capsuleRadius = charCapsuleCollider.radius;
    }

    /// <summary>
    /// アタッチしたモデルを移動させるメソッド
    /// </summary>
    /// <param name="moveVector3"></param>
    /// <param name="isJump"></param>
    public void Move(Vector3 moveVector3, bool isJump)
    {
        if(moveVector3.magnitude > 1.0f) moveVector3.Normalize();
        moveVector3 = transform.InverseTransformDirection(moveVector3);
        
    }

    /// <summary>
    /// 地面の状態を確認するメソッド
    /// </summary>
    private void CheckGroundStatus()
    {
        
    }
}
