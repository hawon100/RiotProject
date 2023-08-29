// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Tilemaps;

// public class CreateMap : MonoBehaviour
// {

//     [SerializeField] private List<Transform> pos = new();
//     [SerializeField] private List<Transform> doorPos = new();
//     [SerializeField] private Transform gruop;
//     [SerializeField] private GameObject wall;

//     [SerializeField] private int repetitionCount;
//     public int walkCount;
//     public int mapSize;

//     private List<Vector2Int> plusPos = new();
//     private Vector3Int leftDown;
//     private bool[,] map;

//     [Header("Up Down Left Right")]
//     public List<bool> openDoor = new();

//     public void Create()
//     {
//         plusPos.Add(Vector2Int.up);
//         plusPos.Add(Vector2Int.down);
//         plusPos.Add(Vector2Int.right);
//         plusPos.Add(Vector2Int.left);

//         map = new bool[mapSize, mapSize];

//         for (int i = 0; i < openDoor.Count; i++)
//             if (openDoor[i])
//                 for (int j = -1; j <= 1; j++)
//                     switch (i)
//                     {
//                         case 0:
//                             map[((mapSize - 1) / 2) + j, mapSize - 1] = true;
//                             map[((mapSize - 1) / 2) + j, mapSize - 2] = true;
//                             if (j == 0) map[((mapSize - 1) / 2) + j, mapSize - 3] = true;
//                             break;
//                         case 1:
//                             map[((mapSize - 1) / 2) + j, 0] = true;
//                             map[((mapSize - 1) / 2) + j, 1] = true;
//                             if (j == 0) map[((mapSize - 1) / 2) + j, 2] = true;
//                             break;
//                         case 2:
//                             map[0, ((mapSize - 1) / 2) + j] = true;
//                             map[1, ((mapSize - 1) / 2) + j] = true;
//                             if (j == 0) map[2, ((mapSize - 1) / 2) + j] = true;
//                             break;
//                         case 3:
//                             map[mapSize - 1, ((mapSize - 1) / 2) + j] = true;
//                             map[mapSize - 2, ((mapSize - 1) / 2) + j] = true;
//                             if (j == 0) map[mapSize - 3, ((mapSize - 1) / 2) + j] = true;
//                             break;
//                     }



//         for (int i = 0; i < repetitionCount; i++)
//         {
//             RandomMove(new((mapSize - 1) / 2, (mapSize - 1) / 2), walkCount);
//         }

//         leftDown = new((int)transform.position.x - (mapSize - 1) / 2, 0, (int)transform.position.z - (mapSize - 1) / 2);

//         for (int i = 0; i < mapSize; i++) for (int j = 0; j < mapSize; j++)
//                 if (!map[i, j]) Instantiate(wall, new(leftDown.x + i, 0, leftDown.z + j), Quaternion.identity, gruop);
//     }

//     private HashSet<Vector2Int> RandomMove(Vector2Int startPos, int walkCount)
//     {
//         HashSet<Vector2Int> path = new();
//         var curPos = startPos;
//         path.Add(startPos);

//         for (int i = 0; i < walkCount; i++)
//         {
//             var checkPos = curPos + plusPos[Random.Range(0, plusPos.Count)];
//             try
//             {
//                 if (!map[checkPos.x, checkPos.y]) map[checkPos.x, checkPos.y] = true;
//             }
//             catch
//             {
//                 continue;
//             }
//             curPos = checkPos;
//         }

//         return path;
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{

    [SerializeField] private List<Transform> pos = new();
    [SerializeField] private List<Transform> doorPos = new();
    [SerializeField] private Transform gruop;
    [SerializeField] private GameObject wall;

    [SerializeField] private int repetitionCount;
    public int walkCount;
    public int mapSize;

    private List<Vector2Int> plusPos = new();
    private Vector2Int center;
    private Vector3Int leftDown;
    private int[,] map;

    [Header("Up Down Left Right")]
    public List<bool> openDoor = new();

    public enum Map
    {
        Null,
        Wall,
        Tile
    }

    public void Create()
    {
        plusPos.Add(Vector2Int.up);
        plusPos.Add(Vector2Int.down);
        plusPos.Add(Vector2Int.right);
        plusPos.Add(Vector2Int.left);
        center = new((mapSize - 1) / 2, (mapSize - 1) / 2);

        map = new int[mapSize, mapSize];

        // for (int i = 0; i < openDoor.Count; i++)
        //     if (openDoor[i])




        for (int i = 0; i < repetitionCount; i++)
        {
            RandomMove(center, walkCount);
        }

        leftDown = new((int)transform.position.x - (mapSize - 1) / 2, 0, (int)transform.position.z - (mapSize - 1) / 2);

        for (int i = 0; i < mapSize; i++) for (int j = 0; j < mapSize; j++)
                if (map[i, j] == (int)Map.Wall) Instantiate(wall, new(leftDown.x + i, 0, leftDown.z + j), Quaternion.identity, gruop);
    }

    private HashSet<Vector2Int> RandomMove(Vector2Int startPos, int walkCount)
    {
        HashSet<Vector2Int> path = new();
        var curPos = startPos;
        path.Add(startPos);

        for (int i = 0; i < walkCount; i++)
        {
            var checkPos = curPos + plusPos[Random.Range(0, plusPos.Count)];
            try
            {
                if (map[checkPos.x, checkPos.y] != (int)Map.Tile) Conversion(checkPos);
            }
            catch { continue; }
            curPos = checkPos;
        }

        return path;
    }

    private void Conversion(Vector2Int checkPos)
    {
        for (int i = -1; i <= 1; i++) for (int j = -1; j <= 1; j++)
            {
                int x = checkPos.x + i;
                int y = checkPos.y + j;
                try
                {
                    if (i == 0 && j == 0) map[x, y] = (int)Map.Tile;
                    else if (map[x, y] != (int)Map.Tile) map[x, y] = (int)Map.Wall;
                }
                catch { continue; }
            }
    }

    private void CreateHallWay(int news, Vector2Int curPos)
    {
        //Up Down Left Right
        Vector2Int plusPos = SetDirection(news);
        while (true)
        {
            try
            {
                Conversion(curPos);
            }
            catch { break; }
        }
    }

    private Vector2Int FindBorder(Vector2Int plusPos)
    {
        Vector2Int curPos = center;

        while (true)
        {
            try
            {
                if (map[curPos.x, curPos.y] == (int)Map.Wall && map[curPos.x + plusPos.x, curPos.y + plusPos.y] != (int)Map.Tile)
                    return curPos;

                curPos += plusPos;
            }
            catch { return curPos; }
        }
    }

    private Vector2Int SetDirection(int news, Vector2Int? removePos = null)
    {
        Vector2Int plusPos = new();

        switch (news)
        {
            case 0: plusPos = Vector2Int.up; break;
            case 1: plusPos = Vector2Int.down; break;
            case 2: plusPos = Vector2Int.left; break;
            case 3: plusPos = Vector2Int.right; break;
        }

        return plusPos;
    }
}

