using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsructionControlScript : MonoBehaviour
{
    [SerializeField]
    private GameObject Panel;
    // Start is called before the first frame update
    void Start()
    {
        Panel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstructionButton()
    {
        Panel.gameObject.SetActive(true);
    }

    public void InstructionCloseButton()
    {
        Panel.gameObject.SetActive(false);
    }

}
