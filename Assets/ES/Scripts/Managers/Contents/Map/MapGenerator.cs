using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private List<StageData> stageList;
    [SerializeField] private StageData curStage;
    [SerializeField] private Player player;
    [SerializeField] private NoteManager noteManager;
    [SerializeField] private MoveManager moveManager;
    [SerializeField] private int stageIndex;
    [SerializeField] private int mapIndex;

    private List<MapData> battleMapData = new();
    private List<GameObject> mapTile = new();
    private List<Obj_Base> mapObj = new();
    private List<Enemy_Base> mapEnemy = new();

    private void Init()
    {
        if (stageList.Count != 0)
        {
            stageIndex = RoundData.Instance.stageIndex;
            mapIndex = RoundData.Instance.mapIndex;
            curStage = stageList[stageIndex];
        }

        battleMapData = curStage.battleMapData.ToList();
        mapTile = curStage.mapTile.ToList();
        mapObj = curStage.mapObj.ToList();
        mapEnemy = curStage.mapEnemy.ToList();
    }

    private void ChoiceMap()
    {
        player.Setting(battleMapData[mapIndex].life);
        noteManager.bpm = battleMapData[mapIndex].bpm;
        CreateMap(battleMapData[mapIndex]);
    }

    private void CreateMap(MapData curMap)
    {
        int[,] curGroundData = LoadCSV.Load(curMap.groundMap);
        int[,] totalData = new int[curGroundData.GetLength(0), curGroundData.GetLength(1)];
        MeshGenerator[,] groundMashMap = new MeshGenerator[curGroundData.GetLength(0), curGroundData.GetLength(1)];
        List<MoveGround_Base> moveGround = new();
        List<MoveGround_Base> onOff = new();

        for (int i = 0; i < curGroundData.GetLength(0); i++) for (int j = 0; j < curGroundData.GetLength(1); j++)
            {
                if (curGroundData[i, j] == 0) continue; // void

                var temp = Instantiate(mapTile[curGroundData[i, j] - 1],
                new Vector3(i, -1, j), Quaternion.identity, transform).GetComponent<MeshGenerator>();
                groundMashMap[i, j] = temp;

                if (temp.TryGetComponent<MoveGround_Base>(out var move))
                {
                    move.curPos = new(i, j);
                    move.index = curGroundData[i, j];

                    if (move.TryGetComponent<OnOff>(out var t)) onOff.Add(t);
                    else if(move.isUse) curGroundData[i, j] = 2;
                    else moveGround.Add(move);
                }
            }

        for (int i = 0; i < curGroundData.GetLength(0); i++) for (int j = 0; j < curGroundData.GetLength(1); j++)
            {
                if (curGroundData[i, j] == 1)
                {
                    if (curGroundData[i, j + 1] != 1) groundMashMap[i, j].up = true;
                    if (curGroundData[i, j - 1] != 1) groundMashMap[i, j].down = true;
                    if (curGroundData[i - 1, j] != 1) groundMashMap[i, j].left = true;
                    if (curGroundData[i + 1, j] != 1) groundMashMap[i, j].right = true;

                    groundMashMap[i, j].MeshGeneration();
                }
            }

        moveManager.MapInit(curGroundData, moveGround, onOff);
        Spawn(curMap, totalData);
    }

    private void Spawn(MapData Map, int[,] curMap)
    {
        Enemy_Base[,] map = new Enemy_Base[curMap.GetLength(0), curMap.GetLength(1)];
        Obj_Base[,] obj = new Obj_Base[curMap.GetLength(0), curMap.GetLength(1)];
        List<MoveObj_Base> moveObj = new();
        List<MoveEnemy_Base> moveEnemy = new();

        int[,] curObjData = LoadCSV.Load(Map.objMap);
        int[,] curEnemyData = LoadCSV.Load(Map.enemyMap);

        for (int i = 0; i < curObjData.GetLength(0); i++) for (int j = 0; j < curObjData.GetLength(1); j++)
            {
                if (curObjData[i, j] == 0) continue; // void
                var temp = Instantiate(mapObj[curObjData[i, j] - 1], new Vector3(i, 0, j),
                Quaternion.identity, transform).GetComponent<Obj_Base>();
                temp.curPos = new(i, j);
                obj[i, j] = temp;

                if (temp.TryGetComponent<MoveObj_Base>(out var move))
                {
                    move.index = curObjData[i, j];
                    moveObj.Add(move);
                }
            }

        for (int i = 0; i < curEnemyData.GetLength(0); i++) for (int j = 0; j < curEnemyData.GetLength(1); j++)
            {
                if (curEnemyData[i, j] <= 1) continue; // void

                var temp = Instantiate(mapEnemy[curEnemyData[i, j] - 2], new Vector3(i, 0, j), Quaternion.identity).GetComponent<Enemy_Base>();
                temp.curPos = new(i, j);
                if (temp.TryGetComponent<MoveEnemy_Base>(out var move))
                {
                    for (int k = -1; k <= 1; k++) for (int l = -1; l <= 1; l++)
                        {
                            if (k != 0 && l != 0) continue;
                            else if (k == 0 && l == 0) continue;

                            int x = i + k;
                            int y = j + l;

                            if (curEnemyData[x, y] == 1)
                            {
                                move.attackPos = new(x, y);
                                moveEnemy.Add(move);
                            }
                        }
                }

                map[i, j] = temp;
            }

        moveManager.MobInit(map, obj, moveObj, moveEnemy, curObjData);
    }

    private void Awake()
    {
        player.Init();
        moveManager.Init();
        Init();
        ChoiceMap();
    }
}
