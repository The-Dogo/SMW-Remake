using UnityEngine;

public class Pelado : MonoBehaviour
{
    public Sprite flatSprite;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (collision.transform.DotTest(transform, Vector2.down))
            {
                Flatten();
            }
            else
            {
              
               player.hit();
            }
        }
    }

    private void Flatten()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flatSprite;
        GetComponent<EntitiesMovement>().enabled = false;
        GetComponent<Animator>().enabled = false;
        Destroy(gameObject, 0.5f);
    }

}
