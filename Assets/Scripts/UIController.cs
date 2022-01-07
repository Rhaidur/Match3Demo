using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    #region Panel Tap To Start
    [Header("Tap To Start Panel")]
    [SerializeField]
    GameObject PanelTapToStart;
    [SerializeField]
    [Tooltip("Tap To Start Button")]
    Button ButtonTapToStart;
    #endregion

    #region Panel In Game
    [Header("In Game Panel")]
    [SerializeField]
    GameObject PanelInGame;
    [SerializeField]
    TextMeshProUGUI textScore;
    #endregion

    #region Panel Win
    [Header("Win Panel")]
    [SerializeField]
    GameObject PanelWin;
    [SerializeField]
    Button ButtonWin;
    #endregion

    #region Panel Lose
    [Header("Lose Panel")]
    [SerializeField]
    GameObject PanelLose;
    [SerializeField]
    Button ButtonLose;
    #endregion

    private void OnEnable()
    {
        ObjectManager.UIController = this;
        EventManager.Start += ClosePanelTapToStart;
        EventManager.Start += ShowPanelInGame;
        EventManager.Win += ClosePanelInGame;
        EventManager.Win += ShowPanelWin;
        EventManager.Fail += ClosePanelInGame;
        EventManager.Fail += ShowPanelLose;
    }
    private void OnDisable()
    {
        ObjectManager.UIController = null;
        EventManager.Start -= ClosePanelTapToStart;
        EventManager.Start -= ShowPanelInGame;
        EventManager.Win -= ClosePanelInGame;
        EventManager.Win -= ShowPanelWin;
        EventManager.Fail -= ClosePanelInGame;
        EventManager.Fail -= ShowPanelLose;
    }

    // Start is called before the first frame update
    void Start()
    {
        ShowPanelTapToStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Tap To Start Panel Methods

    void ShowPanelTapToStart()
    {
        PanelTapToStart.SetActive(true);
    }
    void ClosePanelTapToStart()
    {
        PanelTapToStart.SetActive(false);
    }
    public void ButtonActionTapToStart()
    {
        EventManager.Start();
        Debug.Log("Calisiyor");
    }
    #endregion

    #region In Game Panel Methods

    void ShowPanelInGame()
    {
        PanelInGame.SetActive(true);
    }
    void ClosePanelInGame()
    {
        PanelInGame.SetActive(false);
    }
    public void UpdateText(int totalScore)
    {
        textScore.text = "Score : " + totalScore.ToString();
    }

    #endregion

    #region Win Panel Methods

    void ShowPanelWin()
    {
        PanelWin.SetActive(true);
    }
    void ClosePanelWin()
    {
        PanelWin.SetActive(false);
    }
    public void ButtonActionWin()
    {

    }
    #endregion

    #region Lose Panel Methods

    void ShowPanelLose()
    {
        PanelLose.SetActive(true);
    }
    void ClosePanelLose()
    {
        PanelLose.SetActive(false);
    }
    public void ButtonActionLose()
    {

    }
    #endregion
}
