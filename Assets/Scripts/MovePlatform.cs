using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
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

        if (Vector3.Distance(ChildTrans.localPosition,nextPos) <= 0.1)
        {
            ChangeDestination();
        }
    } 

    private void ChangeDestination()
    {
        nextPos = nextPos != posB ? posB : posA;
    }
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.layer = 9;
            //other.gameObject.GetComponent<SpriteRenderer>().
            other.transform.SetParent(ChildTrans);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        other.transform.SetParent(null);
    }
}
