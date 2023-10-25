using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WallDirection { left, back, right, front }

public class MazeModule : MonoBehaviour
{
    [SerializeField] GameObject[] walls = new GameObject[4];
    ModuleWall[] wallsData = new ModuleWall[4];
    public int posX, posZ;
    public bool visited = false;
    public bool haveFuse = false, haveLights = false;

    private void Awake()
    {
        for (int i = 0; i < wallsData.Length; i++)
        {
            wallsData[i] = walls[i].GetComponent<ModuleWall>();
        }
        posX = (int)transform.position.x / MazeGenerator.MODULE_SIZE;
        posZ = (int)transform.position.z / MazeGenerator.MODULE_SIZE;
    }

    public void SetFuse()
    {
        haveFuse = true;

        List<GameObject> aux = new();
        for (int i = 0; i < wallsData.Length; i++)
        {
            if (wallsData[i] != null) aux.Add(wallsData[i].fuse);
        }

        int rnd = Random.Range(0, aux.Count);
        aux[rnd].SetActive(true);
    }

    public void SetLight()
    {
        haveLights = true;

        if (wallsData[(int)WallDirection.left] != null)
        {
            wallsData[(int)WallDirection.left].lightObjects.SetActive(true);
        }

    }

    public bool IsBorderModule()
    {
        if ((posX == MazeGenerator.instance.maxMazeSize - 1 || posX == 0) || 
            (posZ == MazeGenerator.instance.maxMazeSize - 1 || posZ == 0))
            return true;
        else return false;
    }

    public void DestroyWall(WallDirection dir)
    {
        Destroy(walls[(int)dir]);
        wallsData[(int)dir] = null;
    }

}
