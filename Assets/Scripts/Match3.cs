using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Match3 
{
    public static int [,] gridinfo;
    public static int [,] matchGrids;

    static int horizontalLength = 5;
    static int verticalLength = 5;

    static int sameCountRight = 0;
    static int sameCountUp = 0;

    public static int threeCount;
    public static int fourCount;
    public static int squareCount;
    public static bool hasMatch;


    public static void CheckMatch(int[,] gridData, int lengthVertical, int lengthHorizontal)
    {
        gridinfo = gridData;
        verticalLength = lengthVertical;
        horizontalLength = lengthHorizontal;
        matchGrids = new int[verticalLength, horizontalLength];
        threeCount = 0;
        fourCount = 0;
        squareCount = 0;
        test3matchRightNSquare();
        Test3MatchUp();
        if((squareCount == 0)&&(threeCount == 0)&&(fourCount == 0))
        {
            hasMatch = false;
        }
        else
        {
            hasMatch = true;
        }
    }

    static void test3matchRightNSquare()
    {
        for(int i=0; i<verticalLength; i++)
        {
            sameCountRight = 0;
            for(int j = 0; j<horizontalLength; j++)
            {
                CheckRight(i, j);
                CheckSquare(i, j);
            }
        }
    }


    static void CheckRight(int i, int j)
    {
        if (j < horizontalLength - 1)
        {
           if(gridinfo[i,j] == gridinfo[i,j + 1])
           {
                sameCountRight++;
           }
           else
           {
                SetRight(i, j);
           }
        }
        else
        {
            SetRight(i, j);
        }
    }
    static void SetRight(int i, int j)
    {
        if (sameCountRight >= 2)
        {
            if(sameCountRight == 2)
            {
                threeCount++;
            }
            else
            {
                fourCount++;
            }
            for (int x = 0; x <= sameCountRight; x++)
            {
                if(matchGrids[i,j - x] < sameCountRight)
                {
                    matchGrids[i,j - x] = sameCountRight + 1;
                }
            }
        }
        sameCountRight = 0;
    }
    static void Test3MatchUp()
    {
        for (int i = 0; i < horizontalLength; i++)
        {
            sameCountUp = 0;
            for (int j = 0; j < verticalLength; j++)
            {
                CheckUp(j, i);
            }
        }
    }
    static void CheckUp(int i, int j)
    {
        if(i < verticalLength - 1)
        {
            if(gridinfo[i,j]== gridinfo[i + 1,j])
            {
                sameCountUp++;
            }
            else
            {
                SetUp(i, j);
            }
        }
        else
        {
            SetUp(i, j);
        }
    }
    static void SetUp(int i, int j)
    {
        if (sameCountUp >= 2)
        {
            if(sameCountUp == 2)
            {
                threeCount++;
            }
            else
            {
                fourCount++;
            }
            for (int x = 0; x <= sameCountUp; x++)
            {
                if(matchGrids[i - x, j] < sameCountUp + 1)
                {
                    matchGrids[i - x,j] = sameCountUp + 1;
                }
            }
        }
        sameCountUp = 0;
    }
    static void CheckSquare(int i, int j)
    {
        if((i < verticalLength - 1) && (j < horizontalLength - 1))
        {
            if((gridinfo[i,j] == gridinfo[i+1,j]) && (gridinfo[i,j] == gridinfo[i,j+1]) && (gridinfo[i,j] == gridinfo[i+1,j+1]))
            {
                squareCount++;
                matchGrids[i,  j]   = 4;
                matchGrids[i+1,j]   = 4;
                matchGrids[i,  j+1] = 4;
                matchGrids[i+1,j+1] = 4;
            }
        }
    }
}
