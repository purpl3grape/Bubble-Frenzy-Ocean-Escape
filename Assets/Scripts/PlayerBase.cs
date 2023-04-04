using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : CharacterBase
{
    [SerializeField] private bool m_shoot;
    public Vector2 InputDirection { get; set; }
    [SerializeField] private Vector2 m_inputDirection;
    public Vector3 FirstTouchPosition => m_firstTouchPosition;
    [SerializeField] private Vector3 m_firstTouchPosition;   //First touch position
    public Vector3 LastTouchPosition => m_lastTouchPosition;
    [SerializeField] private Vector3 m_lastTouchPosition;   //Last touch position
    public float DragLimit => m_dragLimit;
    [SerializeField] private float m_dragLimit;  //minimum distance for a swipe to be registered

    [SerializeField] private float m_touchSens = 1000f;  //Touch input may vary, but this value seems right for my Samsung Galaxy Flip phone
    private float lastTimeSinceFiredShot;
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    private Vector3 touchScreenMoveDir;
    private Camera camera;
    private bool flipX;

    private void Awake()
    {
        camera = Camera.main;
#if UNITY_EDITOR
#endif
    }

    void Update()
    {
#if UNITY_EDITOR
        InputDirection = Vector2.zero;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            InputDirection -= Vector2.right;
            SpriteRenderer.flipX = false;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            InputDirection += Vector2.right;
            SpriteRenderer.flipX = true;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) { InputDirection += Vector2.up; }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) { InputDirection -= Vector2.up; }
        
        if (InputDirection != Vector2.zero) { InputDirection.Normalize(); }
        Move(InputDirection, MoveSpeed);

        if (GameManager.Instance.GameOver)
        {
            if (Input.GetMouseButtonDown(0)) { GameManager.Instance.TapToConfirmRestart(); }
        }

#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                fingerDownPosition = touch.position;
                var worldPosition = camera.ScreenToWorldPoint(fingerDownPosition);
                InputDirection = (worldPosition - transform.position).normalized;
                Move(InputDirection, m_touchSens * Time.deltaTime);
            }

            if(touch.phase == TouchPhase.Ended)
            {
                if (GameManager.Instance.GameOver) { GameManager.Instance.TapToConfirmRestart(); }
            }
        }
#endif
    }
}