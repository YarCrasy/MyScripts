using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleWall : MonoBehaviour
{
    public GameObject fuse, lightObjects;
    Light[] lightsData = new Light[2];

    private void Awake()
    {
        for (int i = 0; i < lightsData.Length; i++)
        {
            lightsData[i] = lightObjects.transform.GetChild(i).gameObject.GetComponent<Light>();
        }
    }

    IEnumerator LightFlashes()
    {
        while (true)
        {

            yield return new WaitForSecondsRealtime(Random.Range(0.1f, 2f));
        }
    }

}
