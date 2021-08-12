using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePanel : MonoBehaviour
{
    [SerializeField]
    private GameObject heart1;

    [SerializeField]
    private GameObject heart2;

    [SerializeField]
    private GameObject heart3;
    // Start is called before the first frame update
    void Start()
    {
        heart1.gameObject.SetActive(true);
        heart2.gameObject.SetActive(true);
        heart3.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        LifeControl();
    }
    void LifeControl()
    {
        if(Player.deadCount <= 3)
        {
            switch (Player.deadCount)
            {
                case 1:
                    heart3.gameObject.SetActive(false);
                    break;
                case 2:
                    heart3.gameObject.SetActive(false);
                    heart2.gameObject.SetActive(false);
                    break;
                case 3:
                    heart3.gameObject.SetActive(false);
                    heart2.gameObject.SetActive(false);
                    heart1.gameObject.SetActive(false);
                    break;
            }
        }
        
    }
}
