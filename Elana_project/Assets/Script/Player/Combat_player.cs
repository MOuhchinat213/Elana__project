using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class Combat_player : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Animator anim;
    [SerializeField] private Image manabar;
    [SerializeField] private Image healthbar;

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
        sr = GetComponentInChildren<SpriteRenderer>();
        Health = MaxHealth;
        Mana = 1;
        manabar.fillAmount= Mana;
        healthbar.fillAmount= Health;

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
            isHealing=false;
        }
        manabar.fillAmount= Mana;
        healthbar.fillAmount = Health;
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
        float temp  = rb.gravityScale;
        isHealing=true;
        if(Mana>=0.5f && Health<MaxHealth)
        {
            rb.gravityScale=0;
            anim.SetTrigger("isHealing");
            Mana-=0.5f;
            
            Health+=0.3f;
            rb.gravityScale=temp;
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
