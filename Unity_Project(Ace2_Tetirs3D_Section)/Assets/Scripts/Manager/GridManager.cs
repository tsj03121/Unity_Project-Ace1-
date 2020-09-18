using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridManager : MonoBehaviour
{
    
    int grid_MaxX_ = 10;
    int grid_MaxY_ = 20;

    [SerializeField]
    GameObject[,] grid_;

    [SerializeField]
    Factory blockFactory_;

    public Action LineClearAction;
    public Action BlocksInputAction;
    public Action GameOverAction;

    void Awake()
    {
        grid_ = new GameObject[grid_MaxX_, grid_MaxY_];
    }

    void LineClear(List<GameObject> blocks)
    {
        for (int blockIndex = 0; blockIndex < blocks.Count; ++blockIndex)
        {
            int xCount = 0;
            int y = (int)blocks[blockIndex].transform.position.y;

            for (int x = 0; x < grid_MaxX_; ++x)
            {
                if (grid_[x, y] != null)
                {
                    xCount += 1;
                }
            }

            if(xCount == grid_MaxX_)
            {
                LineClearAction?.Invoke();
                for(int gridY = y; gridY < grid_MaxY_; ++gridY)
                {
                    for (int gridX = 0; gridX < grid_MaxX_; ++gridX)
                    {
                        if(gridY != grid_MaxY_ -1)
                        {
                            if (grid_[gridX, gridY] != null)
                            {
                                blockFactory_.Restore(grid_[gridX, gridY]);
                            }

                            grid_[gridX, gridY] = grid_[gridX, gridY + 1];
                            grid_[gridX, gridY + 1] = null;

                            if (grid_[gridX, gridY] != null)
                            {
                                grid_[gridX, gridY].transform.position += Vector3.down;
                            }
                        }
                        else
                        {
                            grid_[gridX, grid_MaxY_ - 1] = null;
                        }
                    }
                }
            }
        }
    }

    public bool isNextPosMove(List<GameObject> blocks, Vector3 vector3)
    {
        int posX = (int)vector3.x;

        for (int i = 0; i < blocks.Count; ++i)
        {
            int x = (int)blocks[i].transform.position.x + posX;
            int y = (int)blocks[i].transform.position.y;

            if (x > 9 || x < 0 || y > 19 || y < 0 || grid_[x, y] != null)
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

            //돌렸는데 해당위치로 이동이 불가하면
            if (x > 9 || x < 0 || y > 19 || y < 0 || grid_[x, y] != null)
            {
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

            //오른쪽으로 한칸 이동해도 들어가지 않으면
            if (x > 9 || x < 0 || y > 19 || y < 0 || grid_[x, y] != null)
            {
                for (int next_i = 0; next_i < blocks.Count; ++next_i)
                {
                    int next_x = (int)(blocks[next_i].transform.position.x + vector3s[next_i].x - 1);
                    int next_y = (int)(blocks[next_i].transform.position.y + vector3s[next_i].y);

                    //왼쪽으로 한칸 이동해도 들어가지 않으면
                    if (next_x > 9 || next_x < 0 || next_y > 19 || next_y < 0 || grid_[next_x, next_y] != null)
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

            if (y < 0 || grid_[x, y] != null)
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
            int x = (int)blocks[i].transform.position.x;
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

                grid_[x, y] = blocks[i];
            }
        }

        LineClear(blocks);
        BlocksInputAction?.Invoke();
    }

    public void OnGameOver()
    {
        for(int j = 0; j < grid_MaxY_; ++j)
        {
            for (int i = 0; i < grid_MaxX_; ++i)
            {
                if (grid_[i, j] != null)
                {
                    blockFactory_.Restore(grid_[i, j]);
                }
                grid_[i, j] = null;
            }
        }
        GameOverAction?.Invoke();
    }
}
