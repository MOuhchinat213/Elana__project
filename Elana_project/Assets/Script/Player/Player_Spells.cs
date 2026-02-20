using System.Collections;
using UnityEngine;

public class Player_Spells : MonoBehaviour
{
    private Combat_player _player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player=GetComponent<Combat_player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Healing_spell());
            _player.isHealing=false;
        }
        
    }
    IEnumerator Healing_spell()
    {
        float temp  = _player.rb.gravityScale;
        _player.isHealing=true;
        if(_player.Mana>=0.5f && _player.Health<_player.MaxHealth)
        {
            _player.rb.gravityScale=0;
            _player.rb.linearVelocity = Vector2.zero;
            _player.anim.SetTrigger("isHealing");
            yield return null;
            float heal_time= _player.anim.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds (heal_time-0.2f);
            
            _player.Mana-=0.5f;
            
            _player.Health+=1;
            _player.rb.gravityScale=temp;
            
            if(_player.Health>_player.MaxHealth)
                _player.Health=_player.MaxHealth;
        }

    }
}
