using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelStart : MonoBehaviour
{
    [SerializeField]
    private GameObject Panel;

    [SerializeField]
    private Text lev;

    public static int levelNumber = 1;
    // Start is called before the first frame update
    void Start()
    {
        Panel.gameObject.SetActive(true);
        StartCoroutine(Level());
        lev.text = levelNumber == 1 ? "Level "+1.ToString(): "Level "+(levelNumber).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator Level()
    {
        yield return new WaitForSeconds(3);
        levelNumber += 1; 
        ChangeLevel(levelNumber);
        Panel.gameObject.SetActive(false);
        
    }
    
    private void ChangeLevel(int levNumber)
    {
        SceneManager.LoadScene(levNumber);
        
    }
}
