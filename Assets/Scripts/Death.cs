using System.Collections;
using UnityEngine;

public class Death : MonoBehaviour
{
    public SpriteRenderer smallRenderer;
    public Animator smallAnimator;

    private void Reset()
    {
        

    }

    public void Awake()
    {
        
    }
    private void OnEnable()
    {
       
        DisablePhysics();

        smallAnimator.SetBool("Death", true);

         
    }

    private void OnDisable()
    {
        
        StopAllCoroutines();
    }
    
    private void DisablePhysics()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }

        if (TryGetComponent(out Rigidbody2D rigidbody))
        {
            rigidbody.linearVelocity = Vector2.zero;
            rigidbody.bodyType = RigidbodyType2D.Static;
        }

        if (TryGetComponent(out PlayerMovement playerMovement))
        {
            playerMovement.enabled = false;
        }

        if (TryGetComponent(out EntitiesMovement entityMovement))
        {
            entityMovement.enabled = false;
        }

    }

    

}
