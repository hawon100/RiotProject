using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private StageData curStage;
    [SerializeField] private RoundData roundData;
    [SerializeField] private Player player;
    [SerializeField] private int stageIndex;

    [SerializeField] GameObject enemy; //임시, 나중에 EnemyList을 만들던가 아니면 싹 다 랜덤으로 나오게 하던가 할 것
    [SerializeField] int enemyCount; //임시

    private List<MapData> battleMapData = new();
    private List<MapData> specialMapData = new();
    private List<GameObject> mapTile = new();
    private List<GameObject> mapObj = new();
    private AudioClip bgm;

    private void Init()
    {
        stageIndex = RoundData.Instance.stageIndex;

        battleMapData = curStage.battleMapData.ToList();
        // specialMapData = curStage.specialMapData.ToList();
        mapTile = curStage.mapTile.ToList();
        mapObj = curStage.mapObj.ToList();
        // bgm = curStage.bgm;
    }

    private void ChoiceMap(int count)
    {
        if (count == stageIndex) { CreateMap(specialMapData[0]); return; }

        int ranMap = Random.Range(0, battleMapData.Count);
        CreateMap(battleMapData[ranMap]);
        battleMapData.Remove(battleMapData[ranMap]);
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
        Base[,] map = new Base[curMap.GetLength(0), curMap.GetLength(1)];
        List<Vector2Int> spawnPos = new();
        List<Enemy_Base> spawnMob = new();

        int[,] curObjData = LoadCSV.Load(Map.objMap);

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
                temp.curPos = new(i, j);
                map[i, j] = temp;
            }


        for (int i = 0; i < curMap.GetLength(0); i++) for (int j = 0; j < curMap.GetLength(1); j++)
                if (curMap[i, j] == 1) spawnPos.Add(new(i, j));

        for (int i = 0; i < enemyCount; i++)
        {
            int ranPos = Random.Range(0, spawnPos.Count);
            var temp = Instantiate(enemy, new Vector3(spawnPos[ranPos].x, 0, spawnPos[ranPos].y),
            Quaternion.identity).GetComponent<Enemy_Base>();
            temp.curPos = new(spawnPos[ranPos].x, spawnPos[ranPos].y);
            map[spawnPos[ranPos].x, spawnPos[ranPos].y] = temp;
            spawnPos.Remove(spawnPos[ranPos]);
            spawnMob.Add(temp);
        }

        MoveManager.Instance.MobInit(map, spawnMob, curObjData);
    }

    private void Awake() {
        roundData.InitData();
        roundData.Reset();
        player.Init();
        Init();
        ChoiceMap(1);
    }
}
