using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coffinBoxScript : MonoBehaviour
{
    [SerializeField]
    private Transform frontDoor;

    [SerializeField]
    private Transform pos;

    [SerializeField]
    private GameObject rightSide;

    private float speed;

    public static bool isDoormove;

    void Start()
    {
        speed = 2;
        isDoormove = false;
        rightSide.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDoormove)
        {
            StartCoroutine(DoorMove());
        }

        
    }

    IEnumerator DoorMove()
    {
        frontDoor.localPosition = Vector3.MoveTowards(frontDoor.localPosition, pos.localPosition, speed * Time.deltaTime);
        yield return new WaitForSeconds(1);
        rightSide.gameObject.SetActive(true);
    }
}
