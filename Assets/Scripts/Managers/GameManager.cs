using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameStates
    {
        Pending,
        Start,
        Win,
        Fail
    }
    public GameStates gameState;

    private void OnEnable()
    {
        ObjectManager.GameManager = this; 
        EventManager.Pending += PendingGame;
        EventManager.Start += StartGame;
        EventManager.Win += WinGame;
        EventManager.Fail += FailGame;
    }
    private void OnDisable()
    {
        ObjectManager.GameManager = null;
        EventManager.Pending -= PendingGame;
        EventManager.Start -= StartGame;
        EventManager.Win -= WinGame;
        EventManager.Fail -= FailGame;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GeneratePanel()
    {

    }

    #region GameStateChanges
    void PendingGame()
    {
        gameState = GameStates.Pending;
    }

    public void StartGame()
    {
        gameState = GameStates.Start;
    }

    void WinGame()
    {
        gameState = GameStates.Win;
    }

    void FailGame()
    {
        gameState = GameStates.Fail;
    }
    #endregion

}
