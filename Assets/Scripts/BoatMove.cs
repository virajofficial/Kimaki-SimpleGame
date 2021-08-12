using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMove : MonoBehaviour
{
    private Vector3 posA;
    private Vector3 posB;
    private Vector3 nextPos;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform ChildTrans;

    [SerializeField]
    private Transform TransB;

    private bool facingRight;
    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
        posA = ChildTrans.localPosition;
        posB = TransB.localPosition;
        nextPos = posB;
    }

    // Update is called once per frame
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
        //facingRight = !facingRight;
        //ChildTrans.localScale = new Vector3(ChildTrans.localScale.x * -1, 1, 1);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            speed = 3;
            other.gameObject.layer = 9;
            other.transform.SetParent(ChildTrans);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        speed = 0;
        other.transform.SetParent(null);
    }
}
