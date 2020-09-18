using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    GameObject[,,] _grid;

    [SerializeField]
    Factory _blockFactory;

    public Action CallbackLineClear;
    public Action CallbackBlocksInput;
    public Action CallbackGameOver;

    int _grid_MaxX = 10;
    int _grid_MaxY = 20;
    int _grid_MaxZ = 10;

    void Awake()
    {
        _grid = new GameObject[_grid_MaxX, _grid_MaxY, _grid_MaxZ];
    }

    void LineClear(List<GameObject> blocks)
    {
        for (int blockIndex = 0; blockIndex < blocks.Count; ++blockIndex)
        {
            int xCount = 0;
            int y = (int)blocks[blockIndex].transform.position.y;

            for (int z = 0; z < _grid_MaxZ; ++z)
            {
                for (int x = 0; x < _grid_MaxX; ++x)
                {
                    if (_grid[x, y, z] != null)
                    {
                        xCount += 1;
                    }
                }
            }

            if(xCount == _grid_MaxX * _grid_MaxZ)
            {
                CallbackLineClear?.Invoke();
                for(int gridY = y; gridY < _grid_MaxY; ++gridY)
                {
                    for (int gridZ = 0; gridZ < _grid_MaxZ; ++gridZ)
                    {
                        for (int gridX = 0; gridX < _grid_MaxX; ++gridX)
                        {
                            if (gridY != _grid_MaxY - 1)
                            {
                                if (_grid[gridX, gridY, gridZ] != null)
                                {
                                    _blockFactory.Restore(_grid[gridX, gridY, gridZ]);
                                }

                                _grid[gridX, gridY, gridZ] = _grid[gridX, gridY + 1, gridZ];
                                _grid[gridX, gridY + 1, gridZ] = null;

                                if (_grid[gridX, gridY, gridZ] != null)
                                {
                                    _grid[gridX, gridY, gridZ].transform.position += Vector3.down;
                                }
                            }
                            else
                            {
                                _grid[gridX, _grid_MaxY - 1, gridZ] = null;
                            }
                        }
                    }
                }
            }
        }
    }

    public void Init(Factory factory)
    {
        _blockFactory = factory;
    }

    public void OnBlocksGridInput(List<GameObject> blocks)
    {
        bool gameOver = false;
        for (int i = 0; i < blocks.Count; ++i)
        {
            int y = (int)blocks[i].transform.position.y;

            if (y >= _grid_MaxY - 1)
            {
                gameOver = true;
            }
        }

        if (gameOver)
        {
            OnGameOver();
            return;
        }
        else
        {
            for (int i = 0; i < blocks.Count; ++i)
            {
                int x = (int)blocks[i].transform.position.x;
                int y = (int)blocks[i].transform.position.y;
                int z = (int)blocks[i].transform.position.z;

                _grid[x, y, z] = blocks[i];
            }
        }

        LineClear(blocks);
        CallbackBlocksInput?.Invoke();
    }

    public void OnGameOver()
    {
        for(int y = 0; y < _grid_MaxY; ++y)
        {
            for (int x = 0; x < _grid_MaxX; ++x)
            {
                for (int z = 0; z < _grid_MaxZ; ++z)
                {
                    if (_grid[x, y, z] != null)
                    {
                        _blockFactory.Restore(_grid[x, y, z]);
                    }
                    _grid[x, y, z] = null;
                }
            }
        }
        CallbackGameOver?.Invoke();
    }

    public bool OnNextPosMove(List<GameObject> blocks, Vector3 vector3)
    {
        int posX = (int)vector3.x;
        int posZ = (int)vector3.z;

        for (int i = 0; i < blocks.Count; ++i)
        {
            int x = (int)blocks[i].transform.position.x + posX;
            int y = (int)blocks[i].transform.position.y;
            int z = (int)blocks[i].transform.position.z + posZ;


            if (x > 9 || x < 0 || y > 19 || y < 0 || z > 9 || z < 0 || _grid[x, y, z] != null)
                return false;
        }

        return true;
    }

    public bool OnNextRotMove(Vector3[] vector3s)
    {
        for (int i = 0; i < vector3s.Length; ++i)
        {
            int x = (int)(vector3s[i].x);
            int y = (int)(vector3s[i].y);
            int z = (int)(vector3s[i].z);

            //돌렸는데 해당위치로 이동이 불가하면
            if (x > _grid_MaxX - 1 || x < 0 || y > _grid_MaxY - 1 || y < 0 || z > _grid_MaxZ - 1 || z < 0 || _grid[x, y, z] != null)
            {
                //여기서 벡터로 x축회전인지 z축 회전인지 확인하는 벡터를 보내주면 해결할듯 + 1 - 1또한 여기서 정해줘야할듯
                return false;
            }
        }
        return true;
    }

    public bool OnNextPosDown(List<GameObject> blocks)
    {
        for (int i = 0; i < blocks.Count; ++i)
        {
            int x = (int)blocks[i].transform.position.x;
            int y = (int)blocks[i].transform.position.y - 1;
            int z = (int)blocks[i].transform.position.z;

            if (y < 0 || _grid[x, y, z] != null)
            {
                return false;
            }
        }
        return true;
    }

}
