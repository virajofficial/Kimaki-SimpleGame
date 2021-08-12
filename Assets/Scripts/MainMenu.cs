using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource audioSRC;
    public AudioClip clickFx;

    [SerializeField]
    private GameObject LevelsMenu;

    [SerializeField]
    private GameObject Menu;

    [SerializeField]
    private GameObject ClosePopup;

    [SerializeField]
    private GameObject SoundButton;
    // Start is called before the first frame update
    void Start()
    {
        Menu.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        ButtonClickSound();
        SceneManager.LoadScene(1);
    }

    public void LevelsButton()
    {
        ButtonClickSound();
        LevelsMenu.gameObject.SetActive(true);
        Menu.gameObject.SetActive(false);
        SoundButton.gameObject.SetActive(false);
    }

    public void LevelCloseButton()
    {
        ButtonClickSound();
        LevelsMenu.gameObject.SetActive(false);
        Menu.gameObject.SetActive(true);
        SoundButton.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        ButtonClickSound();
        ClosePopup.gameObject.SetActive(true);
        Menu.gameObject.SetActive(false);
        SoundButton.gameObject.SetActive(false);
    }

    public void QuitYes()
    {
        ButtonClickSound();
        Application.Quit();
    }

    public void QuitNo()
    {
        ButtonClickSound();
        ClosePopup.gameObject.SetActive(false);
        Menu.gameObject.SetActive(true);
        SoundButton.gameObject.SetActive(true);
    }

    public void SelectLevel(int level)
    {
        ButtonClickSound();
        LevelStart.levelNumber = level;
        SceneManager.LoadScene(1);
    }

    void ButtonClickSound()
    {
        audioSRC.PlayOneShot(clickFx);
    }
}
