using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeAnimCtrl : MonoBehaviour
{
    public static FadeAnimCtrl instance;
    [SerializeField] Animator anim;

    private void Awake()
    {
        instance = this;
    }

    public void FadeIn(bool set)
    {
        anim.SetTrigger("Fade");
        anim.SetBool("FadeIn", set);
    }
}
