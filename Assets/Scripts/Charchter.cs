using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charchter : MonoBehaviour
{
    [SerializeField]
    protected float speed;
    protected Rigidbody2D RigidBody;
    public Animator Animation;



    private void Awake()
    {

    }
    public virtual void Die()
    {
        RigidBody.gameObject.SetActive(false);
        Rigidbody2D.Destroy(this);
    }
}


