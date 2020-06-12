using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Particle;
    private bool _isTouchAvaliable = false;
    private Vector3 _initalTouchPosiition;
    private Vector3 _endTouchPosiition;
    private float _swipeThreshHold = 20f;
    private Vector3 _playerPosition;
    private long _playerFlipCount;
    void Start()
    {
        _isTouchAvaliable = Input.touchSupported;
        _playerPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CaptainStitch")
        {
            GameManager.GameMangerInstance.ShakeCamera();
            GameManager.GameMangerInstance.UpdateLives();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GameMangerInstance.isGameStarted)
        {
            if (!Particle.activeInHierarchy)
            {
                Particle.SetActive(true);
            }
            
            if (_isTouchAvaliable)
            {
                MoveWithTouch();
            }
            else
            {
                MoveWithMouse();
            }
        }
    }


    private void MoveWithTouch()
    {
        foreach (var touch in Input.touches)
        {
            if(touch.phase == TouchPhase.Began)
            {
                _initalTouchPosiition = touch.position;
                _endTouchPosiition = touch.position;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                _endTouchPosiition = touch.position;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                _endTouchPosiition = touch.position;

                if(Mathf.Abs(_endTouchPosiition.y - _initalTouchPosiition.y) > _swipeThreshHold)
                {
                    if ((_initalTouchPosiition.y - _endTouchPosiition.y) > 0) // down swipe
                    {
                        //add moving cod here
                        Flip();
                    }
                    else if ((_initalTouchPosiition.y - _endTouchPosiition.y) < 0) // up swipe
                    {
                        //add moving cod here
                        Flip();
                    }
                }

            }
        }
    }

    private void MoveWithMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //player position switch
            Flip();
        }
    }

    private void Flip()
    {
        
        if(_playerFlipCount % 2 == 0 || _playerFlipCount == 0)
        {
            transform.position = new Vector3(_playerPosition.x, _playerPosition.y, _playerPosition.z);
            transform.rotation = new Quaternion(0, 0, transform.position.z, 0);
            Particle.transform.position = new Vector3(-5.1f, 0.5f, _playerPosition.z);
        }
        else {
            transform.position = new Vector3(-4.66f, -1.26f, _playerPosition.z);
            transform.rotation = new Quaternion(-173.39f, -15.51f, transform.position.z, 0);
            Particle.transform.position = new Vector3(-5.1f, -1.07f, _playerPosition.z);
        }
                
        _playerFlipCount++;
    }

   
}
