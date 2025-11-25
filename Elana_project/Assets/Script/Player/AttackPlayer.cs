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

    #endregion

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
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
            }
        if(Input.GetKeyDown(KeyCode.R))
        {
            Healing_spell();
        }
    }

    #region Close Range Attack
    
    void Basic_Attack()
    {

         Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitbox.position, range, EnemyLayer);
         anim.SetTrigger("attack");
         foreach (Collider2D enemy in hitEnemies)
        {
            //enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
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
