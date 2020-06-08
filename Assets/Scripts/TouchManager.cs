using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    float speedMultiplier = 5f;
    bool slideStart;
    Vector3 firstTouchPos;
    Vector3 finalTouchPos;
    
    Touch touch;
    // Start is called before the first frame update
    void Start()
    {
        slideStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            //Just one touch we use
            touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    firstTouchPos = Camera.main.ViewportToScreenPoint(touch.position);
                    print(firstTouchPos);
                    break;
                case TouchPhase.Moved:
                    slideStart = true;
                    finalTouchPos = Camera.main.ViewportToScreenPoint(touch.position);
                    print(finalTouchPos);
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    slideStart = false;
                    //StopPlayer();
                    break;
                case TouchPhase.Canceled:
                    break;
                default:
                    break;
            }
        }
    }
    private void FixedUpdate()
    {
        if (slideStart)
        {
            Vector3 offset = finalTouchPos - firstTouchPos;
            Vector3 direction = offset.normalized;
            //float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            //Debug.Log(angle);
            //player.rotation = Quaternion.Euler(0f, angle - 90,0f );
            MovePlayer(-direction);
        }
    }
    void MovePlayer(Vector3 direction)
    {
        player.LookAt(new Vector3(finalTouchPos.x,0f,finalTouchPos.z));
        player.GetComponent<Rigidbody>().velocity = new Vector3(direction.x, 0f, direction.y)*speedMultiplier*Time.deltaTime;
    }
    void StopPlayer()
    {
        
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
