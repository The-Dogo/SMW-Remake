using UnityEngine;

public class SideScrolling : MonoBehaviour
{
    public float cameraSpeed = 2f;

    private Transform player;

    private float startX;

    public float followThreshold = 0f;

    public float smoothTime = 0.2f;

    private Vector3 velocitySmooth = Vector3.zero;

    private bool cameraStartedFollowing = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startX = transform.position.x;
      
        float halfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        followThreshold = startX + halfWidth;


    }

    private void LateUpdate()
    {

        Vector3 currentPos = transform.position;

        if (!cameraStartedFollowing)
        {
            if (player.position.x > followThreshold)
            {
                cameraStartedFollowing = true;
            }
            else
            {
                currentPos.x = startX;
                transform.position = currentPos;
                return;
            }
        }

        float targetX = Mathf.Max(startX, player.position.x);
        Vector3 targetPos = new Vector3(targetX, currentPos.y, currentPos.z);

        transform.position = Vector3.SmoothDamp(currentPos, targetPos, ref velocitySmooth, smoothTime);

    }

}
