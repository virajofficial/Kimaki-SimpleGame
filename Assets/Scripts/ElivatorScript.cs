using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElivatorScript : MonoBehaviour
{
    private Vector3 posA;
    private Vector3 posB;
    private Vector3 nextPos;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform ChildTrans;

    [SerializeField]
    private GameObject TransA;

    [SerializeField]
    private GameObject TransB;

    private bool isOnElivator;
    // Start is called before the first frame update
    void Start()
    {
        posA = TransA.transform.localPosition;
        posB = TransB.transform.localPosition;
        nextPos = posB;
        isOnElivator = false;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {

        ChildTrans.localPosition = Vector3.MoveTowards(ChildTrans.transform.localPosition, nextPos, speed * Time.deltaTime);

        if (Vector3.Distance(ChildTrans.localPosition, nextPos) <= 0.1)
        {
            ChangeDestination();
        }
    }

    private void ChangeDestination()
    {
        nextPos = nextPos != posB ? posB : posA;
        speed = 0;
        //facingRight = !facingRight;
        //ChildTrans.localScale = new Vector3(ChildTrans.localScale.x * -1, 1, 1);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            speed = 3;
            isOnElivator = true;
            other.transform.SetParent(ChildTrans);

            other.gameObject.layer = 9;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        other.transform.SetParent(null);
    }
}
