using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Camera cam;
    public GameObject hitObject;
    Vector2 mousePos;

    Grid currentGrid;
    public enum SwipeStates
    {
        Empty,
        Pending,
        Left,
        Right,
        Down,
        Up, 
        Waiting
    }
    public SwipeStates swipeState;

    private void OnEnable()
    {
        ObjectManager.GameController = this;
    }
    private void OnDisable()
    {
        ObjectManager.GameController = null;
    }
    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if(swipeState != SwipeStates.Waiting)
        {
            MousePosCheck();
            SwipeCheck();
        }
    }

    #region MousePositionCheck Method
    void MousePosCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if ((hit.collider != null)&&(hit.collider.CompareTag("Grid")))
            {
                hitObject = hit.collider.gameObject;
                currentGrid = hitObject.GetComponent<Grid>();
                swipeState = SwipeStates.Pending;
            } 
            
        }
    }
    #endregion

    #region SwipeCheck Method
    void SwipeCheck()
    {
        if (Input.GetMouseButton(0))
        {
            if (swipeState == SwipeStates.Pending)
            {
                Vector2 deltaMousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                Vector2 dirMousePos = deltaMousePos - mousePos;

                if (dirMousePos.x >= 1)
                {
                    Debug.Log("Saga Kaydi");
                    swipeState = SwipeStates.Right;
                }
                else if (dirMousePos.x <= -1)
                {
                    Debug.Log("Sola Kaydi");
                    swipeState = SwipeStates.Left;
                }

                else if (dirMousePos.y >= 1)
                {
                    Debug.Log("Yukari Kaydi");
                    swipeState = SwipeStates.Up;
                }
                else if (dirMousePos.y <= -1)
                {
                    Debug.Log("Asagi Kaydi");
                    swipeState = SwipeStates.Down;
                }

                if((swipeState != SwipeStates.Pending) && (swipeState != SwipeStates.Empty))
                {
                    ObjectManager.GridManager.MoveCheck(swipeState, currentGrid);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            swipeState = SwipeStates.Empty;
        }
    }
    #endregion

}
