using UnityEngine;

public static class Extension 
{
    private static LayerMask layerMask = LayerMask.GetMask("Default");
    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction)
    {
        if (rigidbody.bodyType == RigidbodyType2D.Dynamic
          )
        {
            return false;
        }
          
        float radius = 0.25f;
        float distance = 0.375f;

        RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position,radius,direction,distance);
        return hit.collider != null && hit.rigidbody != rigidbody;
    }

}
