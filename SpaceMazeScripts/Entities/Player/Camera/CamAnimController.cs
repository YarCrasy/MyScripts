using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAnimController : MonoBehaviour
{
    [SerializeField] Animator camAnim;

    private void Update()
    {
        camAnim.SetBool("Idle", !IsMoving());
        camAnim.SetBool("Walk", IsMoving());
        camAnim.SetBool("Run", IsMoving() && Input.GetButton("Run"));
        camAnim.SetBool("Crouch", Input.GetButton("Crouch"));
    }

    bool IsMoving()
    {
        return (Mathf.Abs(Input.GetAxisRaw("Horizontal")) + Mathf.Abs(Input.GetAxisRaw("Vertical"))) > 0;
    }

}
