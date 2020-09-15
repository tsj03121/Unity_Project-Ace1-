using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridManager : MonoBehaviour
{
    
    int grid_MaxX_ = 10;
    int grid_MaxY_ = 20;
    int grid_MaxZ_ = 10;

    [SerializeField]
    GameObject[,,] grid_;

    [SerializeField]
    Factory blockFactory_;

    public Action LineClearAction;
    public Action BlocksInputAction;
    public Action GameOverAction;

    void Awake()
    {
        grid_ = new GameObject[grid_MaxX_, grid_MaxY_, grid_MaxZ_];
    }

    void LineClear(List<GameObject> blocks)
    {
        for (int blockIndex = 0; blockIndex < blocks.Count; ++blockIndex)
        {
            int xCount = 0;
            int y = (int)blocks[blockIndex].transform.position.y;

            for (int z = 0; z < grid_MaxZ_; ++z)
            {
                for (int x = 0; x < grid_MaxX_; ++x)
                {
                    if (grid_[x, y, z] != null)
                    {
                        xCount += 1;
                    }
                }
            }

            if(xCount == grid_MaxX_ * grid_MaxZ_)
            {
                LineClearAction?.Invoke();
                for(int gridY = y; gridY < grid_MaxY_; ++gridY)
                {
                    for (int gridZ = 0; gridZ < grid_MaxZ_; ++gridZ)
                    {
                        for (int gridX = 0; gridX < grid_MaxX_; ++gridX)
                        {
                            if (gridY != grid_MaxY_ - 1)
                            {
                                if (grid_[gridX, gridY, gridZ] != null)
                                {
                                    blockFactory_.Restore(grid_[gridX, gridY, gridZ]);
                                }

                                grid_[gridX, gridY, gridZ] = grid_[gridX, gridY + 1, gridZ];
                                grid_[gridX, gridY + 1, gridZ] = null;

                                if (grid_[gridX, gridY, gridZ] != null)
                                {
                                    grid_[gridX, gridY, gridZ].transform.position += Vector3.down;
                                }
                            }
                            else
                            {
                                grid_[gridX, grid_MaxY_ - 1, gridZ] = null;
                            }
                        }
                    }
                }
            }
        }
    }

    public bool isNextPosMove(List<GameObject> blocks, Vector3 vector3)
    {
        int posX = (int)vector3.x;
        int posZ = (int)vector3.z;

        for (int i = 0; i < blocks.Count; ++i)
        {
            int x = (int)blocks[i].transform.position.x + posX;
            int y = (int)blocks[i].transform.position.y;
            int z = (int)blocks[i].transform.position.z + posZ;


            if (x > 9 || x < 0 || y > 19 || y < 0 || z > 9 || z < 0 || grid_[x, y, z] != null)
                return false;
        }

        return true;
    }

    public bool isNextRotMove(List<GameObject> blocks, Vector3[] vector3s , int value)
    {
        for (int i = 0; i < blocks.Count; ++i)
        {
            int x = (int)(blocks[i].transform.position.x + (vector3s[i].x * value));
            int y = (int)(blocks[i].transform.position.y + (vector3s[i].y * value));
            int z = (int)(blocks[i].transform.position.z + (vector3s[i].z * value));

            //돌렸는데 해당위치로 이동이 불가하면
            if (x > 9 || x < 0 || y > 19 || y < 0 || z > 0 || z > 0 || grid_[x, y, z] != null)
            {
                //여기서 벡터로 x축회전인지 z축 회전인지 확인하는 벡터를 보내주면 해결할듯 + 1 - 1또한 여기서 정해줘야할듯
                return isWallKick(blocks, vector3s);
            }
        }
        return true;
    }

    public bool isWallKick(List<GameObject> blocks, Vector3[] vector3s)
    {
        bool check = true;
        for (int i = 0; i < blocks.Count; ++i)
        {
            int x = (int)(blocks[i].transform.position.x + vector3s[i].x + 1);
            int y = (int)(blocks[i].transform.position.y + vector3s[i].y);
            int z = (int)(blocks[i].transform.position.z + vector3s[i].z + 1);

            //오른쪽으로 한칸 이동해도 들어가지 않으면
            if (x > 9 || x < 0 || y > 19 || y < 0 || z > 9 || z < 0 || grid_[x, y, z] != null)
            {
                for (int next_i = 0; next_i < blocks.Count; ++next_i)
                {
                    int next_x = (int)(blocks[next_i].transform.position.x + vector3s[next_i].x - 1);
                    int next_y = (int)(blocks[next_i].transform.position.y + vector3s[next_i].y);
                    int next_z = (int)(blocks[next_i].transform.position.z + vector3s[next_i].z - 1);

                    //왼쪽으로 한칸 이동해도 들어가지 않으면
                    if (next_x > 9 || next_x < 0 || next_y > 19 || next_y < 0 || next_z > 9 || next_z < 0 || grid_[next_x, next_y, next_z] != null)
                    {
                        return false;
                    }
                    else
                    {
                        check = true;
                    }
                }

                if (check)
                {
                    for (int next_i = 0; next_i < blocks.Count; ++next_i)
                    {
                        blocks[next_i].transform.position += Vector3.left;
                    }
                    return true;
                }
            }
            else
            {
                check = true;
            }
        }

        if (check)
        {
            for (int i = 0; i < blocks.Count; ++i)
            {
                blocks[i].transform.position += Vector3.right;
            }
            return true;
        }

        return false;
    }

    public bool isNextPosDown(List<GameObject> blocks)
    {
        for (int i = 0; i < blocks.Count; ++i)
        {
            int x = (int)blocks[i].transform.position.x;
            int y = (int)blocks[i].transform.position.y - 1;
            int z = (int)blocks[i].transform.position.z;

            if (y < 0 || grid_[x, y, z] != null)
            {
                return false;
            }
        }
        return true;
    }

    public void Init(Factory factory)
    {
        blockFactory_ = factory;
    }

    public void CurrBlocksGridInput(List<GameObject> blocks)
    {
        bool gameOver = false;
        for (int i = 0; i < blocks.Count; ++i)
        {
            int y = (int)blocks[i].transform.position.y;

            if (y >= grid_MaxY_ - 1)
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

                grid_[x, y, z] = blocks[i];
            }
        }

        LineClear(blocks);
        BlocksInputAction?.Invoke();
    }

    public void OnGameOver()
    {
        for(int y = 0; y < grid_MaxY_; ++y)
        {
            for (int x = 0; x < grid_MaxX_; ++x)
            {
                for (int z = 0; z < grid_MaxZ_; ++z)
                {
                    if (grid_[x, y, z] != null)
                    {
                        blockFactory_.Restore(grid_[x, y, z]);
                    }
                    grid_[x, y, z] = null;
                }
            }
        }
        GameOverAction?.Invoke();
    }
}
