using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    const string DEBUGHeader = "<b>[<color=blue>Player Controller</color>]</b> ";

    [Header("Core")]
    public Camera cam;

    [Header("Settings")]
    [SerializeField, Min(0)] float moveSpeed = 35;
    [SerializeField, Min(0)] float topSpeed = 5;
    [SerializeField, Min(0)] float stoppingSpeed = 50;
    
    [Space]
    
    [SerializeField, Range(0, 10)] float mouseLookSensitivity = 2;
    [SerializeField, Range(0, 10)] float stickLookSensitivity = 5;
    
    GameManager gM;
    InputListener input;
    Rigidbody phys;

    Vector2 lookAngles;

    Vector3 startPos;
    Vector2 startRot;

    void Start()
    {
        gM = GameManager.Instance;

        if (gM == null)
        {
            Debug.LogError(DEBUGHeader + "Couldn't find Game Manager, Stopping!");
            enabled = false;
            return;
        }

        input = gM.input;

        phys = GetComponent<Rigidbody>();

        if (cam != null)
        { lookAngles = cam.transform.eulerAngles; }

        startPos = transform.position;
        startRot = lookAngles;
    }

    void Update()
    {
        // Failsafe Respawn
        if (transform.position.y <= -100)
        {
            transform.position = startPos;
            lookAngles = startRot;
            phys.velocity = Vector3.zero;
        }

        Grounded = CheckForGround();

        #region Movement
        Vector3 move = new Vector3(input.gameplay.Movement.x, 0, input.gameplay.Movement.y) * (moveSpeed * Time.deltaTime);
        move = transform.rotation * move;
        if ((phys.velocity + move).magnitude <= topSpeed & input.gameplay.Movement.magnitude >= .1f)
        { phys.velocity += move; }
        else if (phys.velocity.magnitude >= stoppingSpeed * Time.deltaTime)
        { phys.velocity -= phys.velocity.normalized * (stoppingSpeed * Time.deltaTime); }
        else
        { phys.velocity = Vector3.up * phys.velocity.y; }
        #endregion

        #region Look Movement
        #region Mouse Lock
        if (input.gameplay.Grab is InteractionType.Down)
        { Cursor.visible = false; }

        if (Cursor.visible)
        { 
            if (Cursor.lockState != CursorLockMode.None)
            { Cursor.lockState = CursorLockMode.None; }
            return;
        }
        if (Cursor.lockState != CursorLockMode.Locked)
        { Cursor.lockState = CursorLockMode.Locked; }
        #endregion

        lookAngles.y +=
            input.gameplay.MouseDelta.x * mouseLookSensitivity * 45 * Time.deltaTime +
            input.gameplay.Look.x * stickLookSensitivity * Time.deltaTime;

        lookAngles.x +=
            -input.gameplay.MouseDelta.y * mouseLookSensitivity * 45 * Time.deltaTime +
            -input.gameplay.Look.y * stickLookSensitivity * Time.deltaTime;

        lookAngles.y %= 360; // Will keep and overflow value between 0 - 360
        lookAngles.x = Mathf.Clamp(lookAngles.x, -89, 89); // Clamps vertical looking angle

        cam.transform.localEulerAngles = Vector3.right * lookAngles.x;
        transform.eulerAngles = Vector3.up * lookAngles.y;
        #endregion
    }

    RaycastHit groundHit; bool Grounded;
    public bool CheckForGround()
    {
        if (Physics.SphereCast(transform.position + Vector3.up, .35f, Vector3.down, out groundHit, .8f, 1, QueryTriggerInteraction.Ignore))
        {
            // Ground Snap
            transform.position += Vector3.up * (-groundHit.distance + .75f);
            return true;
        }

        return false;
    }    
}