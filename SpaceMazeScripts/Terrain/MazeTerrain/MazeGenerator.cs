using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnergyFuse;

public class MazeGenerator : MonoBehaviour
{
    public static MazeGenerator instance;

    public const int MODULE_SIZE = 6, MAX_PATH_GEN_LOOP = 10000;
    [Range(GameSettingData.MIN_MAZE_SIZE, GameSettingData.MAX_MAZE_SIZE)]public int maxMazeSize;

    [SerializeField] GameObject plane;
    [SerializeField] GameObject exitDoor;
    [SerializeField] MazeModule[] modulesPreset;
    public GameObject[,] maze;
    public MazeModule[,] modules;

    int actualX, actualZ;
    readonly int[] auxX = { -1, 0, 1, 0 }, auxZ = { 0, -1, 0, 1 };
    readonly Stack<MazeModule> pathStack = new();

    private void Awake()
    {
        instance = this;
        maxMazeSize = GameSettingData.instance.maxMazeSize;
        maze = new GameObject[maxMazeSize, maxMazeSize];
        modules = new MazeModule[maxMazeSize, maxMazeSize];

        //inicializo las variable estaticas de Energy aquí para no inicializarlas varias veces en el propio script
        maxFuseInScene = maxMazeSize / 5;
        actualFuseInscene = activatedFuse = 0;
    }

    private void Start()
    {
        SetModules();
        GeneratePath();
        SetPoweredModules();
        //QuitParent();
        //Destroy(gameObject);
    }

    void SetModules()
    {
        for (int i = 0; i < modulesPreset.Length; i++)
        {
            if (modulesPreset[i].posX < maxMazeSize && modulesPreset[i].posZ < maxMazeSize)
            {
                modules[modulesPreset[i].posX, modulesPreset[i].posZ] = modulesPreset[i];
                maze[modulesPreset[i].posX, modulesPreset[i].posZ] = modules[modulesPreset[i].posX, modulesPreset[i].posZ].gameObject;
            }
            else
            {
                Destroy(modulesPreset[i].gameObject);
            }
        }
        modulesPreset = null;
    }

    //si necesitas un mapa mental, tranquilo yo tambien.
    //ves todo eso verde de abajo? pues ahí lo tienes, disfrutalo
    //que está bastante limpio?
    //no dirías eso si ves el codigo espagueti que era antes de la refactorización,
    //casi 500 lineas de nada a base de prints y codigos de prueba. XD
    void GeneratePath()
    {
        //busca primero un modulo aleatorio del laberinto y lo mete en la pila
        actualX = Random.Range(0, maxMazeSize);
        actualZ = Random.Range(0, maxMazeSize);
        pathStack.Push(modules[actualX, actualZ]);

        //un bucle que acaba cuando se han visitado todos los modulos, o por si hay algun fallo, si llega al limite establecido
        for (int limit = 0; limit < MAX_PATH_GEN_LOOP && !AllVisited(); limit++)
        {
            //coge los datos del ultimo modulo del stack, y se marca como visitada
            MazeModule mod = pathStack.Peek();
            actualX = mod.posX;
            actualZ = mod.posZ;
            mod.visited = true;

            //si hay alguna direccion por visitar
            if (!AllDirVisited())
            {
                //escoge una direccion aleatoria
                WallDirection dir = (WallDirection)Random.Range(0, 4);

                //si la posicion de la direccion escogida esta dentro del limite
                if (PosInRange(actualX + auxX[(int)dir], actualZ + auxZ[(int)dir]))
                {
                    //coge el modulo de esa direccion
                    MazeModule next = modules[actualX + auxX[(int)dir], actualZ + auxZ[(int)dir]];

                    //si este modulo no está visitada
                    if (!next.visited)
                    {
                        //destruye las paredes que separan estos dos modulos segun la direccion elegida
                        switch (dir) 
                        {
                            case WallDirection.left:
                                DestroyWall(mod, next, dir, WallDirection.right);
                                break;
                            case WallDirection.back:
                                DestroyWall(mod, next, dir, WallDirection.front);
                                break;
                            case WallDirection.right:
                                DestroyWall(mod, next, dir, WallDirection.left);
                                break;
                            case WallDirection.front:
                                DestroyWall(mod, next, dir, WallDirection.back);
                                break;
                            default:break;
                        }
                        //pone el nuevo modulo en la pila
                        pathStack.Push(next);
                    }
                }
            }
            //si no hay, quita el modulo de la pila
            else
            {
                pathStack.Pop();
            }

        }
    }

    void DestroyWall(MazeModule a, MazeModule b, WallDirection dir, WallDirection opposite)
    {
        a.DestroyWall(dir);
        b.DestroyWall(opposite);
    }

    bool AllVisited()
    {
        if (pathStack.Count == 0) return true;
        else return false;
    }

    bool AllDirVisited()
    {
        bool findNotVisited = true;
        for (int i = 0; i < auxX.Length && findNotVisited; i++)
        {
            if (PosInRange(actualX + auxX[i], actualZ + auxZ[i]))
            {
                if (!modules[actualX + auxX[i], actualZ + auxZ[i]].visited)
                {
                    findNotVisited = false;
                }
            }
        }
        return findNotVisited;
    }

    bool PosInRange(int x, int z)
    {
        if ((x >= 0 && x <= maxMazeSize - 1) && (z >= 0 && z <= maxMazeSize - 1)) return true;
        else return false;
    }

    //void QuitParent()
    //{
    //    plane.transform.parent = null; 
    //    for (int i = 0; i < maxMazeSize; i++)
    //    {
    //        for (int j = 0; j < maxMazeSize; j++)
    //        {
    //            maze[i, j].transform.parent = null;
    //        }
    //    }
    //}

    public int MazeRandomWorldPosition()
    {
        return Random.Range(0, maxMazeSize - 1) * MODULE_SIZE;
    }

    void SetPoweredModules()
    {
        while (actualFuseInscene < maxFuseInScene)
        {
            int x = MazeRandomWorldPosition() / MODULE_SIZE;
            int z = MazeRandomWorldPosition() / MODULE_SIZE;

            int limit = 50;
            while (modules[x, z].IsBorderModule() && !modules[x, z].haveFuse && limit > 0)
            {
                x = MazeRandomWorldPosition() / MODULE_SIZE;
                z = MazeRandomWorldPosition() / MODULE_SIZE;
                limit--;
            }

            modules[x, z].SetFuse();
            actualFuseInscene++;
        }

    }

}
