using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ParticleType { bullet, };

public class ParticleController : MonoBehaviour
{
    public static ParticleController instance;
    public Transform playerTsf;
    [SerializeField] ParticleSystem[] particles;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (playerTsf == null)
        {
            playerTsf = FindObjectOfType<PlayerMove>().transform;
        }
    }

    public void PlayParticleAt(ParticleType type, Transform tsf)
    {
        particles[(int)type].gameObject.transform.position = tsf.position;
        particles[(int)type].gameObject.transform.LookAt(playerTsf);
        particles[(int)type].Play();
    }
    

}
