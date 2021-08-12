using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneDoorScript : MonoBehaviour
{
    private Vector3 topA;
    private Vector3 topB;
    private Vector3 bottomA;
    private Vector3 bottomB;
    private Vector3 nextPos1;
    private Vector3 nextPos2;


    public static float speed;

    [SerializeField]
    private Transform ChildTrans1;

    [SerializeField]
    private Transform ChildTrans2;

    [SerializeField]
    private Transform Trans1;

    [SerializeField]
    private Transform Trans2;

    [SerializeField]
    private SpriteRenderer lockIcon;

    public static bool isDoorOpend;

    // Start is called before the first frame update
    void Start()
    {
        isDoorOpend = false;
        
        speed = 0;
        topA = ChildTrans1.localPosition;
        topB = Trans1.localPosition;
        nextPos1 = topB;

        bottomA = ChildTrans2.localPosition;
        bottomB = Trans2.localPosition;
        nextPos2 = bottomB;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDoorOpend)
        {
            lockIcon.color = new Color(255, 0, 0, 223);
        }
        else
        {
            lockIcon.color = new Color(35, 255, 0, 223);
        }

        Move();

        
    }

    void Move()
    {
        ChildTrans1.localPosition = Vector3.MoveTowards(ChildTrans1.transform.localPosition, nextPos1, speed * Time.deltaTime);
        ChildTrans2.localPosition = Vector3.MoveTowards(ChildTrans2.transform.localPosition, nextPos2, speed * Time.deltaTime);

        if (Vector3.Distance(ChildTrans1.localPosition, nextPos1) <= 0.1)
        {
            ChangeDestination();
        }
    }

    private void ChangeDestination()
    {
        if(nextPos1 != topB && nextPos2 != bottomB)
        {
            nextPos1 = topB;
            nextPos2 = bottomB;
            isDoorOpend = false;
        }
        else
        {
            nextPos1 = topA;
            nextPos2 = bottomA;
            isDoorOpend = true;
        }
        //nextPos1 = nextPos1 != topB ? topB : topA;
        //nextPos2 = nextPos2 != bottomB ? bottomB : bottomA;
        speed = 0;
    }
}
