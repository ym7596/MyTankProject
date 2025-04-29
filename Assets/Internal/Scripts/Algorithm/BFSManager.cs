using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using UnityEngine;

public class BFSManager : MonoBehaviour
{
    [SerializeField] private Transform _root;
    [SerializeField] private GameObject _tilePrefab;

    [SerializeField] private GameObject _tankPrefab;

    private GameObject _myTank;


    private Vector3 _startPos = new Vector3(0, 2, 0);
    private Vector3 _endPos = new Vector3(3, 0, 7);
    /// <summary>
    /// 0 0 0 0
    /// 0 0 0 0
    /// 0 0 0 0
    /// 0 0 0 0
    /// 0 0 0 1
    /// 0 0 0 0
    /// 0 0 0 0
    /// 0 0 0 0
    /// </summary>

    public GameObject[,] _gameObjectPos;
    private int[,] _isVisited;
    private (int xx, int yy)[,] _parents;

    private int _row = 4;
    private int _col = 8;

    private float _speed = 2f;
    private bool _isMoving = false;
    private int currentIndex = 0;

    private Vector3 _targetPos;

    private List<(int, int)> _path;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetMap();

        _myTank = Instantiate(_tankPrefab, _gameObjectPos[0, 0].transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        BFS();

        var start = (x: (int) _startPos.x, y: (int) _startPos.z);
        var end = (x: (int) _endPos.x, y: (int) _endPos.z);
        _path = ReconstructPath(start, end);


        foreach(var p in _path)
        {
            Debug.Log(p);
        }

        if(_path.Count > 0)
        {
            currentIndex = 0;
            _isMoving = true;
            _targetPos = ConvertToWorldPos(_path[currentIndex]);
        }
    }

    private void SetMap()
    {
        _parents = new (int xx, int yy)[_row, _col];
        _gameObjectPos = new GameObject[_row, _col];
        _isVisited = new int[_row, _col];
        for (int i = 0; i < _col; i++)
        {
            for(int j =0; j< _row; j++)
            {
                _gameObjectPos[j, i] = Instantiate(_tilePrefab, _root);
                _gameObjectPos[j, i].transform.localPosition = new Vector3(j * 2, 0, i * 2);

                if(i==3 && j == 3)
                {
                    _isVisited[j, i] = 1;
                    _gameObjectPos[j,i].gameObject.GetComponent<Renderer>().material.color = Color.red;
                }
                else
                    _isVisited[j, i] = 0;
            }
        }
    }

    private Vector3 ConvertToWorldPos((int x, int z) gridPos)
    {
        return new Vector3(_gameObjectPos[gridPos.x, gridPos.z].transform.position.x
            , 2,
            _gameObjectPos[gridPos.x, gridPos.z].transform.position.z
            );
    }

    private void BFS()
    {
        // 너비 우선 탐색
        Queue<(int, int)> queue = new Queue<(int, int)>();

        queue.Enqueue((0, 0));
        _isVisited[0, 0] = 1;
        _parents[0, 0] = (-1, -1);

        while (queue.Count > 0)
        {
            var (x,y) = queue.Dequeue();

           // Debug.Log($"x : {x} y: {y}");
            if ((x, y) == (_row - 1, _col - 1))
            {
                Debug.Log("Finish");
                break;
            }
               

            //위 아래 오른쪽 왼쪽

            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            for(int i = 0; i < 4; i++)
            {
                int nx = x + dx[i];
                int ny = y + dy[i];

                if(nx >= 0 && ny >= 0 && nx < _row && ny <_col && _isVisited[nx,ny] == 0)
                {
                    queue.Enqueue((nx, ny));
                    _isVisited[nx,ny] = 1;
                    _parents[nx, ny] = (x, y);
                }
            }
        }

    }

    List<(int, int)> ReconstructPath ((int x, int y) start, (int x, int y) end)
    {
        List<(int,int)> path = new List<(int,int)> ();

        var current = end;

        if (_isVisited[end.x,end.y] == 0)
        {
            return path;
        }

        while(current != (-1, -1))
        {
            path.Add(current);
            current = _parents[current.x, current.y];
        }

        path.Reverse ();
        return path;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMoving == false || _path == null || currentIndex >= _path.Count)
            return;

        _myTank.transform.position =
            Vector3.MoveTowards(_myTank.transform.position, _targetPos, _speed * Time.deltaTime);

        Vector3 dir = (_targetPos - _myTank.transform .position).normalized;
        if(dir != Vector3.zero) //같은방향으로 가는게 아니라면
        {
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            _myTank.transform.rotation = Quaternion.Slerp(
                _myTank.transform.rotation,
                targetRotation,
                10f * Time.deltaTime );
        }


        if(Vector3.Distance(_myTank.transform.position , _targetPos) < 0.01f)
        {
            currentIndex++;
            if(currentIndex < _path.Count)
            {
                _targetPos = ConvertToWorldPos(_path[currentIndex]);
            }
            else
            {
                _isMoving = false;
            }
        }
    }
}
