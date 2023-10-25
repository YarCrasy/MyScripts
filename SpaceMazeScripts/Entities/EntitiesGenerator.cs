using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesGenerator : MonoBehaviour
{
    [SerializeField] GameObject player, enemy;
    [SerializeField] GameObject[] recollectables;

    int[] x, z;

    private void Awake()
    {
        x = z = new int[recollectables.Length];
        InstantiatePlayer();
        InstantiateEnemy();
        InstantiateRecollectables();
    }

    void InstantiatePlayer()
    {
        int aux = (MazeGenerator.instance.maxMazeSize - 1) *MazeGenerator.MODULE_SIZE;
        Vector3 pos = new(aux, 0.5f, aux);
        Instantiate(player, pos, Quaternion.identity);
    }

    void InstantiateEnemy()
    {
        int aux = (MazeGenerator.instance.maxMazeSize - 1) * MazeGenerator.MODULE_SIZE / 2;
        Vector3 pos = new Vector3(aux, 0.5f, aux);
        Instantiate(enemy, pos, Quaternion.identity);
    }

    void InstantiateRecollectables()
    {
        for (int i = 0; i < recollectables.Length; i++) //generate
        {
            x[i] = MazeGenerator.instance.MazeRandomWorldPosition();
            z[i] = MazeGenerator.instance.MazeRandomWorldPosition();
            for (int j = 0; j < recollectables.Length; j++) //check position
            {
                int count = 0;
                while ((j != i) && ((x[i] == x[j]) && (z[i] == z[j])) && count < 20) //if same position set other position
                {
                    x[i] = MazeGenerator.instance.MazeRandomWorldPosition();
                    z[i] = MazeGenerator.instance.MazeRandomWorldPosition();
                    count++;
                }
            }

            Vector3 pos;
            pos.x = Random.Range(x[i] - 2f, x[i] + 2f);
            pos.y = 0.5f;
            pos.z = Random.Range(z[i] - 2f, z[i] + 2f);
            Instantiate(recollectables[i], pos, Quaternion.identity);
        }
    }

}
