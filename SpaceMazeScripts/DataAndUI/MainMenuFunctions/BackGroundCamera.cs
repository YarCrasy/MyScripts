using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundCamera : MonoBehaviour
{
    public static BackGroundCamera instance;

    [SerializeField] GameObject bgCam;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CamToRandomPosition();
    }

    public void CamToRandomPosition()
    {
        bgCam.transform.position = new Vector3(MazeGenerator.instance.MazeRandomWorldPosition(), 2, MazeGenerator.instance.MazeRandomWorldPosition());
    }

    public void FadeIn()
    {
        Debug.Log("hi");
        FadeAnimCtrl.instance.FadeIn(true);
    }

    public void FadeOut()
    {
        FadeAnimCtrl.instance.FadeIn(false);
    }

}
