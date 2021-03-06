using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour, Iuseable
{
    [SerializeField]
    private Collider2D platformCollider;

    public void Use()
    {
        Debug.Log("Ladder Use");
        if (Player.Instance.OnLadder)
        {
            UseLadder(false,5,0,1,"land");
        }
        else
        {
            UseLadder(true,0,1, 0, "reset");
            Physics2D.IgnoreCollision(Player.Instance.GetComponent<Collider2D>(), platformCollider, true);
        }
    }

    private void UseLadder(bool onLadder, int gravity, int layerWeight, int animSpeed, string trigger)
    {
        Player.Instance.OnLadder = onLadder;
        Player.Instance.MyRigidBody.gravityScale = gravity;
        Player.Instance.MyAnimator.SetLayerWeight(2, layerWeight);
        Player.Instance.MyAnimator.speed = animSpeed;
        Player.Instance.MyAnimator.SetTrigger(trigger);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            UseLadder(false, 5, 0,1,"land");
            Physics2D.IgnoreCollision(Player.Instance.GetComponent<Collider2D>(), platformCollider, false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
