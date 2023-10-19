using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private List<StageData> stageList = new();
    [SerializeField] private StageData curStage;
    [SerializeField] private int stageLevel;

    [SerializeField] GameObject enemy; //임시, 나중에 Enemy맵을 만들던가 아니면 싹 다 랜덤으로 나오게 하던가 할 것
    [SerializeField] int enemyCount; //임시

    private List<MapData> battleMapData = new();
    private List<MapData> specialMapData = new();
    private List<GameObject> mapTile = new();
    private List<GameObject> mapObj = new();
    private AudioClip bgm;
    private int stageCount;

    private void Init()
    {
        //curStage = stageList[stageLevel];

        battleMapData = curStage.battleMapData.ToList();
        // specialMapData = curStage.specialMapData.ToList();
        mapTile = curStage.mapTile.ToList();
        mapObj = curStage.mapObj.ToList();
        // bgm = curStage.bgm;
        stageCount = curStage.stageCount;
    }

    private void ChoiceMap(int count)
    {
        if (count == stageCount) return;

        int ranMap = Random.Range(0, battleMapData.Count);
        CreateMap(battleMapData[ranMap]);
    }

    private void CreateMap(MapData curMap)
    {
        int[,] curGroundData = LoadCSV.Load(curMap.groundMap);
        int[,] curObjData = LoadCSV.Load(curMap.objMap);
        int[,] totalData = new int[curGroundData.GetLength(0), curGroundData.GetLength(1)];

        // Debug.Log($"curGroundData {curGroundData.GetLength(0)} {curGroundData.GetLength(1)}");
        // Debug.Log($"curObjData {curObjData.GetLength(0)} {curObjData.GetLength(1)}");
        // Debug.Log($"totalData {totalData.GetLength(0)} {totalData.GetLength(1)}");

        for (int i = 0; i < curGroundData.GetLength(0); i++) for (int j = 0; j < curGroundData.GetLength(1); j++)
            {
                if (curGroundData[i, j] == 0) continue; // void
                Instantiate(mapTile[curGroundData[i, j] - 1], new Vector3(j, -1, curGroundData.GetLength(0) - i - 1), Quaternion.identity, transform);
                totalData[i, j] = 1;
            }

        for (int i = 0; i < curGroundData.GetLength(0); i++) for (int j = 0; j < curObjData.GetLength(1); j++)
            {
                if (curObjData[i, j] == 0) continue; // void
                if (curObjData[i, j] == 1) {
                    Debug.Log($"{i} {j}");
                    totalData[i, j] = 0;
                    for (int k = -1; k <= 1; k++) for (int l = -1; l <= 1; l++)
                        {
                            try { totalData[i + k, j + l] = 0; }
                            catch { continue; }
                        }
                }
                else { Debug.Log($"{i} {j}"); totalData[i, j] = 0; }
                Instantiate(mapObj[curObjData[i, j] - 1], new Vector3(j, 0, curGroundData.GetLength(0) - i - 1), Quaternion.identity, transform);
            }

        MoveManager.Instance.MapInit(curObjData, curGroundData);
        Spawn(totalData);
    }

    private void Spawn(int[,] curMap)
    {
        int[,] spawnMap = new int[curMap.GetLength(0), curMap.GetLength(1)];
        List<Vector2Int> spawnPos = new();
        
        //Debug.Log($"spawnMap {spawnMap.GetLength(0)} {spawnMap.GetLength(1)}");

        for (int i = 0; i < curMap.GetLength(0); i++) for (int j = 0; j < curMap.GetLength(1); j++)
                if (curMap[i, j] == 1) spawnPos.Add(new(j, curMap.GetLength(0) - i - 1));

        for(int i = 0; i < enemyCount; i++){
            int ranPos = Random.Range(0, spawnPos.Count);
            //Debug.Log($"spawnPos {spawnPos[ranPos]}");
            spawnMap[spawnPos[ranPos].x, spawnPos[ranPos].y] = 1;
            Instantiate(enemy, new Vector3(spawnPos[ranPos].x, 0, spawnPos[ranPos].y), Quaternion.identity);
            spawnPos.Remove(spawnPos[ranPos]);
        }

        MoveManager.Instance.MonsterInit(spawnMap);
    }

    private void Start()
    {
        Init();
        ChoiceMap(1);
    }
}
