using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private List<StageData> stageList;
    [SerializeField] private StageData curStage;
    [SerializeField] private Player player;
    [SerializeField] private int stageIndex;


    private List<MapData> battleMapData = new();
    private List<MapData> specialMapData = new();
    private List<GameObject> mapTile = new();
    private List<Obj_Base> mapObj = new();
    private List<Enemy_Base> mapEnemy = new();
    private int enemyCount;

    private AudioClip bgm;

    private void Init()
    {
        // if (stageList.Count != 0)
        // {
        //     stageIndex = RoundData.Instance.stageIndex;
        //     curStage = stageList[stageIndex];
        // }

        stageIndex = RoundData.Instance.mapIndex;

        battleMapData = curStage.battleMapData.ToList();
        // specialMapData = curStage.specialMapData.ToList();
        mapTile = curStage.mapTile.ToList();
        mapObj = curStage.mapObj.ToList();
        mapEnemy = curStage.mapEnemy.ToList();
    }

    private void ChoiceMap(int count)
    {
        // if (count == stageIndex) { CreateMap(specialMapData[0]); return; }

        // int ranMap = Random.Range(0, battleMapData.Count);
        // CreateMap(battleMapData[ranMap]);
        // battleMapData.Remove(battleMapData[ranMap]);

        CreateMap(battleMapData[stageIndex]);
    }

    private void CreateMap(MapData curMap)
    {
        int[,] curGroundData = LoadCSV.Load(curMap.groundMap);
        int[,] totalData = new int[curGroundData.GetLength(0), curGroundData.GetLength(1)];

        for (int i = 0; i < curGroundData.GetLength(0); i++) for (int j = 0; j < curGroundData.GetLength(1); j++)
            {
                if (curGroundData[i, j] == 0) continue; // void
                Instantiate(mapTile[curGroundData[i, j] - 1], new Vector3(i, -1, j), Quaternion.identity, transform);
                totalData[i, j] = 1;
            }

        MoveManager.Instance.MapInit(curGroundData);
        Spawn(curMap, totalData);
    }

    private void Spawn(MapData Map, int[,] curMap)
    {
        Enemy_Base[,] map = new Enemy_Base[curMap.GetLength(0), curMap.GetLength(1)];
        Obj_Base[,] obj = new Obj_Base[curMap.GetLength(0), curMap.GetLength(1)];
        List<Enemy_Base> spawnMob = new();

        int[,] curObjData = LoadCSV.Load(Map.objMap);
        int[,] curEnemyData = LoadCSV.Load(Map.enemyMap);

        for (int i = 0; i < curObjData.GetLength(0); i++) for (int j = 0; j < curObjData.GetLength(1); j++)
            {
                if (curObjData[i, j] == 0) continue; // void
                if (curObjData[i, j] == 1 || curObjData[i, j] == 2)
                {
                    curMap[i, j] = 0;
                    if (curObjData[i, j] == 1)
                        for (int k = -1; k <= 1; k++) for (int l = -1; l <= 1; l++)
                            {
                                try { curMap[i + k, j + l] = 0; }
                                catch { continue; }
                            }

                }
                else { curMap[i, j] = 0; }
                var temp = Instantiate(mapObj[curObjData[i, j] - 1], new Vector3(i, 0, j),
                Quaternion.identity, transform).GetComponent<Obj_Base>();
                temp.index = curObjData[i, j];
                temp.curPos = new(i, j);
                obj[i, j] = temp;
            }

        for (int i = 0; i < curEnemyData.GetLength(0); i++) for (int j = 0; j < curEnemyData.GetLength(1); j++)
            {
                if (curEnemyData[i, j] == 0) continue; // void

                var temp = Instantiate(mapEnemy[curEnemyData[i, j] - 1], new Vector3(i, 0, j), Quaternion.identity).GetComponent<Enemy_Base>();
                temp.curPos = new(i, j);
                map[i, j] = temp;
                spawnMob.Add(temp);
            }

        MoveManager.Instance.MobInit(map, obj, spawnMob, curObjData);
    }

    private void Awake()
    {
        player.Init();
        Init();
        ChoiceMap(stageIndex);
    }
}
