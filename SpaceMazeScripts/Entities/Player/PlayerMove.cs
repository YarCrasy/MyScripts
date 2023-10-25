using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    const int MAX_STAMINA = 50;
    const float STAMINA_USAGE = 15f, STAMINA_RECOVER = 10f;

    Vector3 moveInput, aux;

    [SerializeField] EntityStatsScriptable pStats;
    [SerializeField] Slider staminaBar;
    [SerializeField] Animator camAnim;
    [SerializeField] CamAnimController camAnimController;
    [SerializeField] WalkingSoundsController walkSound;

    Rigidbody rb;
    bool grounded = true;
    bool canRun = true, recoveringStamina = false, zeroStamina = false;
    float stamina = MAX_STAMINA;

    float timer = 0;
    readonly float maxTimer = 0.5f;
    bool timerStarted = false;

    bool firstGround = false;

    private void Awake()
    {
        staminaBar.maxValue = MAX_STAMINA;
        if(rb == null) rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        StaminaCheck();
        HorizontalMove();
        Jump();
    }

    void HorizontalMove()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.z = Input.GetAxis("Vertical");

        aux = transform.forward * moveInput.z + transform.right * moveInput.x;
        aux = aux.normalized;
        aux *= pStats.movementSpeed;
        aux.y = rb.velocity.y;

        if (IsMoving())
        {
            if (Input.GetButton("Crouch"))
            {
                aux.x /= pStats.movementMultiplier;
                aux.z /= pStats.movementMultiplier;
            }
            else if (Input.GetButton("Run") && canRun && !zeroStamina)
            {
                stamina += -STAMINA_USAGE * Time.deltaTime;
                staminaBar.value = stamina;
                if (stamina <= 0)
                {
                    canRun = false;
                    camAnim.SetBool("CanRun", canRun);
                    zeroStamina = true;
                    recoveringStamina = true;
                }
                aux.x *= pStats.movementMultiplier;
                aux.z *= pStats.movementMultiplier;
            }

            rb.velocity = aux;
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

    }

    bool IsMoving()
    {
        return (Mathf.Abs(Input.GetAxisRaw("Horizontal")) + Mathf.Abs(Input.GetAxisRaw("Vertical"))) > 0;
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.velocity = new(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * pStats.jumpForce);
        }
    }

    void StaminaCheck()
    {
        if (Input.GetButtonUp("Run"))
        {
            timerStarted = true;
        }
        else if (Input.GetButtonDown("Run") && IsMoving())
        {
            timerStarted = false;
            timer = 0;
        }

        if (timerStarted)
        {
            timer += Time.deltaTime;
            if (timer >= maxTimer)
            {
                timer = 0;
                recoveringStamina = true;
            }
        }
        else
        {
            recoveringStamina = false;
        }

        if (recoveringStamina) SetRecoverStamina(STAMINA_RECOVER * Time.deltaTime);
        if (stamina >= MAX_STAMINA) canRun = true;
        staminaBar.value = stamina;
        camAnim.SetBool("CanRun", canRun);
    }

    void SetRecoverStamina(float set)
    {
        if (stamina < MAX_STAMINA)
        {
            stamina += set;
        }
        else
        {
            zeroStamina = false;
            recoveringStamina = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject colObject = collision.gameObject;
        if (colObject.layer == 3)
        {
            grounded = true;
            camAnim.enabled = true;

            if(!firstGround) firstGround = true;
            else walkSound.PlayRndAudio();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        GameObject colObject = collision.gameObject;
        if (colObject.layer == 3)
        {
            grounded = false;
            camAnim.enabled = false;
        }
    }
}
