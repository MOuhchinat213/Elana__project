using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class Combat_player : MonoBehaviour
{

    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public Animator anim;


    #region Stat
    public float Health;
    public float MaxHealth = 1;
    public float Mana;

    #endregion

    #region Close Combat
    public float attack_dmg=1f;
    public LayerMask EnemyLayer;
    public float range=0.5f;
    public Transform hitbox;
    public bool isAttacking;
    public bool isHealing;

    #endregion

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        GetComponentInChildren<SpriteRenderer>();
        Health = MaxHealth;
        Mana = 1;
       


    }

    void Update()
    {
        if(Health<=0)
            return;
        if(Input.GetMouseButtonDown(0))
        {
            Basic_Attack();
            isAttacking=false;
        }

        

    }

    #region Close Range Attack

    void Basic_Attack()
    {
        isAttacking=true;
        anim.SetTrigger("attack");
    }
    

    
    #endregion
    
    

}
