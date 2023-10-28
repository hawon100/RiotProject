using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalk : MonoBehaviour
{
    [SerializeField] private GameObject gruop;
    [SerializeField] private GameObject cube;
    private GameObject parent = null;

    [SerializeField] private Vector3Int startPos = Vector3Int.zero;
    [SerializeField] private int repetitionCount;
    [SerializeField] private int walkCount;
    [SerializeField] private bool test;

    private List<Vector3Int> plusPos = new();

    private void Start()
    {
        plusPos.Add(Vector3Int.forward);
        plusPos.Add(Vector3Int.back);
        plusPos.Add(Vector3Int.right);
        plusPos.Add(Vector3Int.left);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.T)){
            if(parent != null) Destroy(parent);

            parent = Instantiate(gruop, new Vector3(0,0,0), Quaternion.identity);
            foreach(var i in StartRandomWalk()){
                Instantiate(cube, i, Quaternion.identity, parent.transform);
            }
        }
    }

    private HashSet<Vector3Int> StartRandomWalk()
    {
        Vector3Int curPos = startPos;
        HashSet<Vector3Int> checkPos = new();

        for (int i = 0; i < repetitionCount; i++)
        {
            var path = RandomMove(curPos, walkCount);
            checkPos.UnionWith(path);
            if (test)
                curPos = checkPos.ElementAt(Random.Range(0, checkPos.Count));
        }
        return checkPos;
    }

    private HashSet<Vector3Int> RandomMove(Vector3Int startPos, int walkCount)
    {
        HashSet<Vector3Int> path = new();
        var curPos = startPos;
        path.Add(startPos);

        for (int i = 0; i < walkCount; i++)
        {
            var checkPos = curPos + plusPos[Random.Range(0, plusPos.Count)];
            path.Add(checkPos);
            curPos = checkPos;
        }

        return path;
    }
}
