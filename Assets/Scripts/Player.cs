using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
   
    public SpriteRenderer smallRenderer;
    public SpriteRenderer bigRenderer;
    public Death deathAnimation;


    public bool isBig = false;

    private void Awake()
    {
        deathAnimation = GetComponent<Death>();

        smallRenderer = GetComponentInChildren<SpriteRenderer>();
        bigRenderer = GetComponentInChildren<SpriteRenderer>();

    }

     public void hit()
    {
        Debug.Log("panhei");

        if (isBig)
        {

            Shrink();

            
        }
        else
        {
            Death();
        }
        
    }

       /* private void grow()
        {
            smallRenderer.enabled = false;
            bigRenderer.enabled = true;

            activeRenderer = bigRenderer;
        }*/

        private void Shrink()
        {
        
        }

        private void Death()
        {

            deathAnimation.enabled = true;

            GameManager.Instance.ResetLevel(2f);
        } 
    }


