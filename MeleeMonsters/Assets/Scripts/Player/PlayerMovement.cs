using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private PhotonView photonView;

    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;

    public MonsterScriptableObject settings;

    internal bool isJumping;
    private int maxJump;
    internal int nbrJump;
    private float jumpForce;
    private float jumpTime;

    internal float moveInputx;
    internal float moveInputy;
    internal bool facingRight = true;
    internal float direction = 1;

    internal bool wallSliding;
    private float wallSlidingSpeed;

    private bool wallJumping;
    public float xWallForce;
    private float yWallForce;
    public float wallJumpTime;

    internal bool isDashing;
    internal int nbrDash;
    private int maxDash;
    private float dashForce;
    private float dashTime;
    internal float dashInputx;
    internal float dashInputy;

    internal bool isFastFalling;
    private float fastFallingSpeed;

    private float moveSpeed;

    public bool isGrounded;
    internal bool isTouchingFront;
    public bool isOnPlatform;
    public bool HasPassedPlatform;

    public bool isPressingDown;

    private PlayerScript playerScript;
    private MonstersAttacks mA;

    internal bool inWave = false;
    internal GameObject wave;

    internal bool isDashAttacking;
    internal float dashAttackSpeed = 0;
    internal Vector2 dashAttackDirection;

    internal bool isFastFallingAttacking;

    public ParticleSystem footSteps;
    public ParticleSystem impactEffect;
    public ParticleSystem dashEffect;
    private bool wasOnGround = false;
    private bool wasOnPlatform = false;

    void Start()
    {
        playerScript = GetComponent<PlayerScript>();
        mA = GetComponent<MonstersAttacks>();
        moveSpeed = settings.speed;
        jumpForce = settings.jumpStrength;
        maxJump = settings.extraJump;
        nbrJump = maxJump;
        wallSlidingSpeed = settings.wallSlidingSpeed;
        yWallForce = settings.yWallForce;
        rb.gravityScale = settings.gravityScale;
        dashForce = settings.dashForce;
        fastFallingSpeed = settings.fastFallingSpeed;
        maxDash = settings.dashNbr;
        nbrDash = maxDash;
        dashTime = settings.dashTime;
        jumpTime = settings.jumpTime;
    }


    private void Update()
    {
        bool isPlaying = (playerScript.currentState == PlayerScript.States.Playing);

        if (photonView.IsMine && isPlaying)
        {
            if (moveInputy > 0)
                print("j'appuie vers le haut là");


            if(inWave)
            {
                transform.position = wave.transform.position;
                transform.Rotate(Vector3.forward, Time.deltaTime * 80);
            }
            else
            {

                if(isDashAttacking)
                {
                    rb.velocity = new Vector2(dashAttackDirection.x*direction*dashAttackSpeed, dashAttackDirection.y*dashAttackSpeed);
                }

                if (isGrounded || isOnPlatform)
                {
                    nbrJump = maxJump;
                    nbrDash = maxDash;
                    isFastFalling = false;
                    if (!isOnPlatform)
                        gameObject.layer = 9;
                }

                if (isTouchingFront && !isGrounded && !isOnPlatform && moveInputx != 0)
                    wallSliding = true;
                else
                    wallSliding = false;

                if (wallSliding)
                {
                    nbrJump = maxJump;
                    nbrDash = maxDash;
                    rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
                }

                if (wallJumping)
                {
                    rb.velocity = new Vector2(xWallForce * -moveInputx, yWallForce);
                }

                if (isDashing)
                {
                    rb.velocity = new Vector2(dashInputx * dashForce, (dashInputy * dashForce) / 2);
                    isFastFalling = false;
                }

                if (isJumping)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    isFastFalling = false;
                    gameObject.layer = 9;
                }

                if (isFastFalling)
                {
                    rb.velocity = new Vector2(rb.velocity.x, -fastFallingSpeed);
                }

                if (isDashAttacking)
                {
                    rb.velocity = new Vector2(dashAttackDirection.x * direction * dashAttackSpeed, dashAttackDirection.y * dashAttackSpeed);
                }

                if (isFastFallingAttacking)
                {
                    rb.velocity = new Vector2(0, -20);
                    if(isGrounded || isOnPlatform)
                    {
                        isFastFallingAttacking = false;
                        mA.FastFallAttackCallback();
                    }
                }

                if (HasPassedPlatform)
                {
                    gameObject.layer = 9;
                }

                if(footSteps != null)
                {
                    if (moveInputx != 0 && (isGrounded||isOnPlatform))
                    {
                        photonView.RPC("ShowFootStepParticles", RpcTarget.All, true);
                    }
                    else
                    {
                        photonView.RPC("ShowFootStepParticles", RpcTarget.All, false);
                    }
                }

                if((isGrounded!=wasOnGround || isOnPlatform!=wasOnPlatform) && impactEffect != null)
                {
                    photonView.RPC("PlayImpactEffect", RpcTarget.All);
                }

                wasOnPlatform = isOnPlatform;
                wasOnGround = isGrounded;
            }
        }
    }


    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            MovePlayerUpdate();

            if (!facingRight && moveInputx > 0)
            {
                Flip();
            }
            else if (facingRight && moveInputx < 0)
            {
                Flip();
            }
        }
    }


    public void MovePlayerUpdate()
    {
        float horizontalMovement = moveInputx * moveSpeed * Time.deltaTime;
        if(!playerScript.isHitStun)
            Move(horizontalMovement);
    }


    void Move(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);
    }


    public void Flip()
    {
        facingRight = !facingRight;
        direction = -1 * direction;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
        if(dashEffect != null)
        {
            if (facingRight)
                dashEffect.gameObject.transform.rotation = Quaternion.Euler(0, 0, -20);
            else
                dashEffect.gameObject.transform.rotation = Quaternion.Euler(0, 0, 200);
        }
    }




    public void WallJumpState()
    {
        wallJumping = true;
        Invoke("SetWallJumpinToFalse", wallJumpTime);
    }
    void SetWallJumpinToFalse()
    {
        wallJumping = false;
    }





    public void DashState()
    {
        if (nbrDash > 0)
        {
            isDashing = true;
            if (dashEffect != null)
                photonView.RPC("PlayDashEffect", RpcTarget.All);
            nbrDash--;
            Invoke("SetDashingToFalse", dashTime);
        }
    }
    void SetDashingToFalse()
    {
        isDashing = false;
        dashInputy = 0;
    }

    public void FastFallState()
    {
        isFastFalling = true;
    }




    public void JumpState()
    {
        if (nbrJump > 0 && !isJumping)
        {
            isJumping = true;
            nbrJump--;
            Invoke("SetJumpingToFalse", jumpTime);
        }
        if (wallSliding)
        {
            WallJumpState();
        }
    }
    void SetJumpingToFalse()
    {
        isJumping = false;
    }




    public void TransparentState()
    {
        gameObject.layer = 11;
        Invoke("NotTransparentAnymore", 0.5f);
    }

    void NotTransparentAnymore()
    {
        gameObject.layer = 9;
    }




    public void Eject(Vector2 force)
    {
        //rb.velocity = Vector2.zero;
        rb.AddForce(force);
    }





    public void FollowWave(bool follow, string waveName)
    {
        photonView.RPC("FollowWaveRPC", RpcTarget.All, follow, waveName);
    }

    [PunRPC]
    public void FollowWaveRPC(bool follow, string waveName)
    {
        inWave = follow;
        if (follow)
            wave = GameObject.Find(waveName);
        else
        {
            wave = null;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }



    public void SetDashAttackState(float waitingTime, float time, float speed, Vector2 direction)
    {
        StartCoroutine(DashAttackState(waitingTime,time,speed,direction));
    }

    IEnumerator DashAttackState(float waitingTime, float time, float speed, Vector2 direction)
    {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(waitingTime);

        isDashAttacking = true;
        dashAttackSpeed = speed;
        dashAttackDirection = direction;
        if (dashEffect != null)
            photonView.RPC("PlayDashEffect", RpcTarget.All);

        yield return new WaitForSeconds(time);
        isDashAttacking = false;
    }

    public void SetFastFallAttack()
    {
        isFastFallingAttacking = true;
    }

    [PunRPC]
    public void ShowFootStepParticles(bool show)
    {
        if (show)
        {
            if(footSteps.isStopped)
                footSteps.Play();
        }
        else
            footSteps.Stop();
    }

    [PunRPC]
    public void PlayImpactEffect()
    {
        impactEffect.gameObject.SetActive(true);
        impactEffect.Stop();
        impactEffect.transform.position = footSteps.transform.position;
        impactEffect.Play();
    }

    [PunRPC]
    public void PlayDashEffect()
    {
        dashEffect.gameObject.SetActive(true);
        dashEffect.Stop();
        dashEffect.transform.position = footSteps.transform.position;
        dashEffect.Play();
    }
}
