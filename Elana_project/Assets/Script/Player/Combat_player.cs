using System;
using UnityEngine;

public class Combat_player : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Animator anim;

    #region Stat
    public int Health;
    public int MaxHealth = 5;
    public int Mana;

    #endregion

    #region Close Combat
    public float attack_dmg=1f;
    public LayerMask EnemyLayer;
    public float range=0.5f;
    public Transform hitbox;
    public bool isAttacking;

    #endregion

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        Health = MaxHealth;
        Mana = 30;

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
        if(Input.GetKeyDown(KeyCode.R))
        {
            Healing_spell();
        }
    }

    #region Close Range Attack

    void Basic_Attack()
    {
        isAttacking=true;
        anim.SetTrigger("attack");
    }
    

    
    #endregion
    
    
    #region Spells
    void Healing_spell()
    {

        if(Mana>=15 && Health<MaxHealth)
        {
            Mana-=15;
            Health+=2;
            if(Health>MaxHealth)
                Health=MaxHealth;
        }
        else
        {
            return;
        }
    }
    #endregion
}
