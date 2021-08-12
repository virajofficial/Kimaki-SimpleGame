using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    [SerializeField]
    private GameObject ControlCanvas;

    [SerializeField]
    private GameObject WinCanvas;

    [SerializeField]
    private GameObject LostCanvas;

    [SerializeField]
    private GameObject earnedCoinsNote;

    [SerializeField]
    private int targetCoins;
    // Start is called before the first frame update
    void Start()
    {
        WinCanvas.gameObject.SetActive(false);
        LostCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.lifeOver)
        {
            Time.timeScale = 0;
            ControlCanvas.gameObject.SetActive(false);
            earnedCoinsNote.gameObject.SetActive(false);
            LostCanvas.gameObject.SetActive(true);
            Player.lifeOver = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            ControlCanvas.gameObject.SetActive(false);
            earnedCoinsNote.gameObject.SetActive(false);
            Time.timeScale = 0;
            // SceneManager.LoadScene(1);
            if (Player.totalCoins >= targetCoins)
            {
                WinCanvas.gameObject.SetActive(true);
            }
            else
            {
                LostCanvas.gameObject.SetActive(true);
            }
        }
        
    }

    private IEnumerator Level1()
    {

        yield return new WaitForSeconds(3);

       //Level1Start.gameObject.SetActive(false);
    }
}
