using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2.5f;
    public bool vertical;
    public float changeTime = 3.0f;
    public ParticleSystem smokeEffect;
    
    Rigidbody2D rigidbody2d;
    float timer;
    int direction = 1;
    Animator animator;
    bool broken = true;

    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
            //remember ! inverse the test, so if broken is true !broken will be false and return won’t be executed.
        if(!broken)
        {
            return;
        }
        
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }
    
    void FixedUpdate()
    {
      if(!broken)
      {
          return;
      }
    
        Vector2 position = rigidbody2d.position;
        
        if(vertical)
        {
        animator.SetFloat("Move X", 0);
        animator.SetFloat("Move Y", direction); 
        position.y = position.y + speed * Time.deltaTime * direction;
        }
        else
        {
        animator.SetFloat("Move X", direction);
        animator.SetFloat("Move Y", 0);
        position.x = position.x + speed * Time.deltaTime * direction;
        }
        rigidbody2d.MovePosition(position);
    }    
    
    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }
    
        //Public because we want to call it from elsewhere like the projectile script
    public void Fix()
    {
        broken = false;
        GetComponent<Rigidbody2D>().simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();

    }
}
