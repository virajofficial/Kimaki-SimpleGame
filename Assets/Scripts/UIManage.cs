using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManage : MonoBehaviour
{
    public static int buildIndex;

    [SerializeField]
    private GameObject PauseMenu;

    [SerializeField]
    private GameObject ControlCanvas;

    [SerializeField]
    private GameObject CloseGamePopup;

    [SerializeField]
    private Text CoinCount;

    [SerializeField]
    private Text FinalCoinsWin;

    [SerializeField]
    private Text FinalCoinsLost;

    [SerializeField]
    private GameObject startNotification;

    [SerializeField]
    private GameObject earnedCoinNotification;

    [SerializeField]
    private int targetCoins;

    [SerializeField]
    private Text earnedConis;

    [SerializeField]
    private GameObject instructionLabel;

    private bool isCoinNoteClosed;
    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        //Level1Start.gameObject.SetActive(true);
        buildIndex = currentScene.buildIndex;
        StartCoroutine(StartNote());
        Debug.Log("build index"+buildIndex);
        LevelStart.levelNumber = buildIndex;
        isCoinNoteClosed = false;
    }
    // Update is called once per frame
    void Update()
    {
        CoinCount.text = Player.totalCoins.ToString();
        FinalCoinsWin.text = Player.totalCoins.ToString();
        FinalCoinsLost.text = Player.totalCoins.ToString();
        earnedConis.text = Player.totalCoins.ToString();
        
    }
    private void FixedUpdate()
    {
        if (Player.totalCoins >= targetCoins && !isCoinNoteClosed )
        {
            StartCoroutine(EarnNote());
        }
    }
    private IEnumerator Level1()
    {
        
        yield return new WaitForSeconds(3);

        //Level1Start.gameObject.SetActive(false);
    }
    public void CloseGame()
    {
        CloseGamePopup.gameObject.SetActive(true);
        ControlCanvas.gameObject.SetActive(false);
        PauseMenu.gameObject.SetActive(false);
    }

    public void QuitNo()
    {
        CloseGamePopup.gameObject.SetActive(false);
        ControlCanvas.gameObject.SetActive(false);
        PauseMenu.gameObject.SetActive(true);
    }
    public void QuitYes()
    {
        Debug.Log("Application Quit");
        Application.Quit();
        
    }
    public void NextLevel()
    {
        LevelStart.levelNumber = buildIndex ;
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void Restart()
    {
        Time.timeScale = 1;
        LevelStart.levelNumber = buildIndex - 1;
        SceneManager.LoadScene(1);
        //Level1Start.gameObject.SetActive(false);
    }
    

    public void GoToHome()
    {
        Time.timeScale = 1;
        LevelStart.levelNumber = 1;
        SceneManager.LoadScene(0);
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
        ControlCanvas.gameObject.SetActive(false);
        PauseMenu.gameObject.SetActive(true);
        startNotification.gameObject.SetActive(false);
        earnedCoinNotification.gameObject.SetActive(false);
        instructionLabel.gameObject.SetActive(false);
    }
    public void PauseMenuClose()
    {
        Time.timeScale = 1;
        ControlCanvas.gameObject.SetActive(true);
        PauseMenu.gameObject.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        ControlCanvas.gameObject.SetActive(true);
        PauseMenu.gameObject.SetActive(false);  
    }

    public void Button1()
    {
        Debug.Log("sfsgdfgdfgdfgd");
    }

    IEnumerator StartNote()
    {
        yield return new WaitForSeconds(1);
        startNotification.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        startNotification.gameObject.SetActive(false);
        instructionLabel.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        instructionLabel.gameObject.SetActive(false);
    }
    
     IEnumerator EarnNote()
    {
        earnedCoinNotification.gameObject.SetActive(true);

        yield return new WaitForSeconds(4);
        earnedCoinNotification.gameObject.SetActive(false);
        isCoinNoteClosed = true;
    }

    
}
