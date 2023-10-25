using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] BoxCollider interationTrigger;
    [SerializeField] AudioSource aud;

    bool opened = false;

    private void OnTriggerEnter(Collider other)
    {
        GameObject colObject = other.gameObject;
        if (colObject.layer == 6)
        {

        }
    }

    public void Open()
    {
        aud.Play();
        opened = true;
        anim.SetBool("OpenDoor", opened);
    }

    public void Close()
    {
        aud.Play();
        opened = false;
        anim.SetBool("OpenDoor", opened);
    }
}
