using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class CharacterMover : NetworkBehaviour
{
    protected Animator animator;

    private bool isMoveable;
    public bool IsMovealbe
    {
        get { return isMoveable; }
        set
        {
            if(!value)
            {
                animator.SetBool("isMove", false);
            }
            isMoveable = value;
        }
    }

    [SyncVar]
    public float speed = 2f;

    [SerializeField]
    private float characterSize = 0.5f;

    [SerializeField]
    private float cameraSize = 2.5f;

    private GameObject joyStickUI;

    protected SpriteRenderer spriteRenderer;

    [SyncVar(hook =nameof(SetPlayerColor_Hook))]
    public EPlayerColor playerColor;

    public void SetPlayerColor_Hook(EPlayerColor oldColor, EPlayerColor newColor)
    {
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        spriteRenderer.material.SetColor("_PlayerColor", PlayerColor.GetColor(newColor));
    }

    [SyncVar(hook = nameof(SetNickname_Hook))]
    public string nickname;
    [SerializeField]
    protected Text nicknameText;
    public void SetNickname_Hook(string _, string value)
    {
        nicknameText.text = value;
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.SetColor("_PlayerColor", PlayerColor.GetColor(playerColor));

        joyStickUI = GameObject.Find("Joystick UI");

        animator = GetComponent<Animator>();
        if(hasAuthority)
        {
            Camera cam = Camera.main;
            cam.transform.SetParent(transform);
            cam.transform.localPosition = new Vector3(0f, 0f, -10f);
            cam.orthographicSize = cameraSize;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        if(hasAuthority && IsMovealbe)
        {
            bool isMove = false;
            if(PlayerSettings.controlType == EControlType.KeyboardMouse)
            {
                joyStickUI.SetActive(false);
                Vector3 dir = Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f), 1f);
                if (dir.x < 0f) transform.localScale = new Vector3(-characterSize, characterSize, 1f);
                else if (dir.x > 0f) transform.localScale = new Vector3(characterSize, characterSize, 1f);
                transform.position += dir * speed * Time.deltaTime;
                isMove = dir.magnitude != 0f;
            }
            else if(PlayerSettings.controlType == EControlType.Mouse)
            {
                joyStickUI.SetActive(false);
                Vector3 dir = (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f)).normalized;
                if (dir.x < 0f) transform.localScale = new Vector3(-characterSize, characterSize, 1f);
                else if (dir.x > 0f) transform.localScale = new Vector3(characterSize, characterSize, 1f);
                transform.position += dir * speed * Time.deltaTime;
                isMove = dir.magnitude != 0f;
            }
            else
            {
                joyStickUI.SetActive(true);
                if(joyStickUI != null)
                {
                    JoyStickUI movementJoyStick = joyStickUI.GetComponent<JoyStickUI>();

                    Vector3 dir = new Vector3(movementJoyStick.joyStickVec.x * speed, movementJoyStick.joyStickVec.y * speed, 0f);
                    if (dir.x < 0f) transform.localScale = new Vector3(-characterSize, characterSize, 1f);
                    else if (dir.x > 0f) transform.localScale = new Vector3(characterSize, characterSize, 1f);
                    transform.position += dir * Time.deltaTime;
                    isMove = dir.magnitude != 0f;

                    /*
                    if (movementJoyStick.joyStickVec.y != 0)
                    {
                        isMove = true;
                        Rigidbody2D rb = GetComponent<Rigidbody2D>();
                        Vector3 dir = new Vector3(movementJoyStick.joyStickVec.x * speed, movementJoyStick.joyStickVec.y * speed, 0f);
                        rb.velocity = new Vector2(movementJoyStick.joyStickVec.x * speed, movementJoyStick.joyStickVec.y * speed);
                    }
                    else
                    {

                    }
                    */
                }
            }
            animator.SetBool("isMove", isMove);
        }
        else
        {
            joyStickUI.SetActive(false);
        }

        if(transform.localScale.x < 0)
        {
            nicknameText.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if(transform.localScale.x > 0)
        {
            nicknameText.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
