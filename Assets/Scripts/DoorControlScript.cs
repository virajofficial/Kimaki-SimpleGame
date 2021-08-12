using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControlScript : MonoBehaviour
{
    private static Animator doorAnim;
    private static BoxCollider2D doorCollider;

    [SerializeField]
    private static bool isDoorOpen;
    // Start is called before the first frame update
    void Start()
    {
        doorAnim = GetComponent<Animator>();
        doorCollider = GetComponent<BoxCollider2D>();
        isDoorOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void DoorControl(bool isdooropen)
    {
        if (isdooropen)
        {
            isDoorOpen = !isDoorOpen;
            if (isDoorOpen == true)
            {
                doorAnim.SetBool("opens", true);
                doorCollider.isTrigger = true;
            }
            else
            {
                doorAnim.SetBool("opens", false);
                doorCollider.isTrigger = false;
            }
        }
    }
}
