using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GridManager : MonoBehaviour
{
    /*
     * Grid Values Begins From 0
     * -1 Value Means That Space is empty/null
     * -13 Value Means That Spcae is Brokeable
     * -3 Value Means That Space is 4Brokeable
     */

    
    [SerializeField] private int verticalLength;  //dikey uzunluk
    [SerializeField] private int horizontaLength; //yatay uzunluk
    [SerializeField] private Transform gridContainer;
    [SerializeField] private float moveTime;
    [SerializeField] private List<GameObject> gridPrefabList;

    [Header("Score Settings")]
    [SerializeField] private int threeMatchPoint;
    [SerializeField] private int fourMatchPoint;
    [SerializeField] private int squareMatchPoint;
    int totalScore;

    int[,] gridArray;
    Grid[,] gridObjectArray;

    int tempRandom;
    
    GameObject tempGrid;
    Vector3 tempPositon;

    WaitForSeconds fillWaitTime = new WaitForSeconds(0.5f);
    bool needRecall;
    bool fillingGapsComplete;

    private void OnEnable()
    {
        ObjectManager.GridManager = this;
        EventManager.Start += GenerateTable;
    }

    private void OnDisable()
    {
        ObjectManager.GridManager = null;
        EventManager.Start -= GenerateTable;
    }

    void GenerateTable()
    {
        gridArray = new int[verticalLength, horizontaLength];
        gridObjectArray = new Grid[verticalLength, horizontaLength];
        for(int i = 0; i<verticalLength; i++)
        {
            for(int j = 0; j<horizontaLength; j++)
            {
                InstantiateGrid(i, j);
            }
        }
    }

    void InstantiateGrid(int i, int j)
    {
        tempRandom = Random.Range(0, gridPrefabList.Count);
        gridArray[i, j] = tempRandom;
        tempPositon = Vector3.up * i + Vector3.right * j;
        tempGrid = Instantiate(gridPrefabList[tempRandom], tempPositon, Quaternion.identity, gridContainer);
        gridObjectArray[i, j] = tempGrid.GetComponent<Grid>();
        gridObjectArray[i, j].verticalIndex = i;
        gridObjectArray[i, j].horizontalIndex = j;
    }

    public void MoveCheck(GameController.SwipeStates swipeState, Grid targetGrid)
    {
        ObjectManager.GameController.swipeState = GameController.SwipeStates.Waiting;
        Grid effectedGrid;
        if(swipeState == GameController.SwipeStates.Left)
        {
            if(targetGrid.horizontalIndex > 0)
            {
                effectedGrid = gridObjectArray[targetGrid.verticalIndex, targetGrid.horizontalIndex - 1];

                effectedGrid.transform.DOMove(effectedGrid.transform.position + Vector3.right, moveTime);
                targetGrid.transform.DOMove(targetGrid.transform.position - Vector3.right, moveTime).OnComplete(()=> 
                    {
                        ChangeGridInfos(effectedGrid, targetGrid);

                        Match3.CheckMatch(gridArray, verticalLength, horizontaLength);
                        if (Match3.hasMatch)
                        {
                            Debug.Log("Doðru Çalýþtý");
                            DestroyGridMatch();
                        }
                        else
                        {
                            Debug.Log("Tersine Dön");
                            ChangeGridInfos(effectedGrid, targetGrid);
                            effectedGrid.transform.DOMove(effectedGrid.transform.position - Vector3.right, moveTime);
                            targetGrid.transform.DOMove(targetGrid.transform.position + Vector3.right, moveTime).OnComplete(()=> 
                                {
                                    ObjectManager.GameController.swipeState = GameController.SwipeStates.Empty;
                                });
                        }
                    });
                
            }
        }
        else if (swipeState == GameController.SwipeStates.Right)
        {
            if(targetGrid.horizontalIndex < horizontaLength - 1)
            {
                effectedGrid = gridObjectArray[targetGrid.verticalIndex, targetGrid.horizontalIndex + 1];

                effectedGrid.transform.DOMove(effectedGrid.transform.position + Vector3.left, moveTime);
                targetGrid.transform.DOMove(targetGrid.transform.position - Vector3.left, moveTime).OnComplete(()=>
                {
                    ChangeGridInfos(effectedGrid, targetGrid);

                    Match3.CheckMatch(gridArray, verticalLength, horizontaLength);
                    if (Match3.hasMatch)
                    {
                        Debug.Log("Doðru Çalýþtý");
                        DestroyGridMatch();
                    }
                    else
                    {
                        Debug.Log("Tersine Dön");
                        ChangeGridInfos(effectedGrid, targetGrid);
                        effectedGrid.transform.DOMove(effectedGrid.transform.position - Vector3.left, moveTime);
                        targetGrid.transform.DOMove(targetGrid.transform.position + Vector3.left, moveTime).OnComplete(() =>
                        {
                            ObjectManager.GameController.swipeState = GameController.SwipeStates.Empty;
                        });
                    }
                });

                
            }
        }
        else if (swipeState == GameController.SwipeStates.Up)
        {
            if(targetGrid.verticalIndex < verticalLength - 1)
            {
                effectedGrid = gridObjectArray[targetGrid.verticalIndex + 1, targetGrid.horizontalIndex];
                effectedGrid.transform.DOMove(effectedGrid.transform.position + Vector3.down, moveTime);
                targetGrid.transform.DOMove(targetGrid.transform.position - Vector3.down, moveTime).OnComplete(() =>
                {
                    ChangeGridInfos(effectedGrid, targetGrid);

                    Match3.CheckMatch(gridArray, verticalLength, horizontaLength);
                    if (Match3.hasMatch)
                    {
                        Debug.Log("Doðru Çalýþtý");
                        DestroyGridMatch();
                    }
                    else
                    {
                        Debug.Log("Tersine Dön");
                        ChangeGridInfos(effectedGrid, targetGrid);
                        effectedGrid.transform.DOMove(effectedGrid.transform.position - Vector3.down, moveTime);
                        targetGrid.transform.DOMove(targetGrid.transform.position + Vector3.down, moveTime).OnComplete(() =>
                        {
                            ObjectManager.GameController.swipeState = GameController.SwipeStates.Empty;
                        });
                    }
                });
            }
        }
        else if (swipeState == GameController.SwipeStates.Down)
        {
            if(targetGrid.verticalIndex > 0)
            {
                effectedGrid = gridObjectArray[targetGrid.verticalIndex - 1, targetGrid.horizontalIndex];
                effectedGrid.transform.DOMove(effectedGrid.transform.position + Vector3.up, moveTime);
                targetGrid.transform.DOMove(targetGrid.transform.position - Vector3.up, moveTime).OnComplete(() =>
                {
                    ChangeGridInfos(effectedGrid, targetGrid);

                    Match3.CheckMatch(gridArray, verticalLength, horizontaLength);
                    if (Match3.hasMatch)
                    {
                        Debug.Log("Doðru Çalýþtý");
                        DestroyGridMatch();
                    }
                    else
                    {
                        Debug.Log("Tersine Dön");
                        ChangeGridInfos(effectedGrid, targetGrid);
                        effectedGrid.transform.DOMove(effectedGrid.transform.position - Vector3.up, moveTime);
                        targetGrid.transform.DOMove(targetGrid.transform.position + Vector3.up, moveTime).OnComplete(() =>
                        {
                            ObjectManager.GameController.swipeState = GameController.SwipeStates.Empty;
                        });
                    }
                });

                
            }
        }
    }

    void ChangeGridInfos(Grid effectedGrid, Grid targetGrid)
    {
        int tempVertical;
        int tempHorizontal;

        tempVertical = effectedGrid.verticalIndex;
        tempHorizontal = effectedGrid.horizontalIndex;

        effectedGrid.verticalIndex = targetGrid.verticalIndex;
        effectedGrid.horizontalIndex = targetGrid.horizontalIndex;

        targetGrid.verticalIndex = tempVertical;
        targetGrid.horizontalIndex = tempHorizontal;

        gridObjectArray[targetGrid.verticalIndex, targetGrid.horizontalIndex] = targetGrid;
        gridArray[targetGrid.verticalIndex, targetGrid.horizontalIndex] = targetGrid.value;
        gridObjectArray[effectedGrid.verticalIndex, effectedGrid.horizontalIndex] = effectedGrid;
        gridArray[effectedGrid.verticalIndex, effectedGrid.horizontalIndex] = effectedGrid.value;
    }
    void FillGaps()
    {
        fillingGapsComplete = false;
        needRecall = false;
        for(int j = 0; j < horizontaLength; j++)
        {
            for(int i = 0; i < verticalLength; i++)
            {
                if(gridArray[i,j] == -1)
                {
                    InstantiateGrid(i, j);
                    needRecall = true;
                    break;
                }
            }
        }
        if (needRecall)
        {
            StartCoroutine(DelayedFillGaps());
        }
        else
        {
            fillingGapsComplete = true;
        }
        
    }
    IEnumerator DelayedFillGaps()
    {
        yield return fillWaitTime;
        FillGaps();
    }

    void DestroyGridMatch()
    {
        for (int i = 0; i < verticalLength; i++)
        {
            for (int j = 0; j < horizontaLength; j++)
            {
                if(Match3.matchGrids[i, j] != 0)
                {
                    Debug.Log(gridObjectArray[i, j]);
                    Destroy(gridObjectArray[i, j].gameObject);
                    gridArray[i, j] = -1;
                }
            }
        }
        UpdateScore();
        FillGaps();
        StartCoroutine(IsGapsFilled());
    }
    IEnumerator IsGapsFilled()
    {
        yield return new WaitUntil(() => fillingGapsComplete);
        Match3.CheckMatch(gridArray, verticalLength, horizontaLength);
        if (Match3.hasMatch)
        {
            Debug.Log("Doðru Çalýþtý");
            DestroyGridMatch();
        }
        else
        {
            ObjectManager.GameController.swipeState = GameController.SwipeStates.Empty;
        }

    }
    void UpdateScore()
    {
        totalScore += Match3.threeCount * threeMatchPoint;
        totalScore += Match3.fourCount * fourMatchPoint;
        totalScore += Match3.squareCount * squareMatchPoint;
        ObjectManager.UIController.UpdateText(totalScore);
    }
}
