using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputListener : MonoBehaviour
{
    public MainInputActions inputActions;

    // Button Key:
    // 0 - Not Pressed
    // 1 - Pressed that Frame
    // 2 - Held Down
    [System.Serializable]
    public class Gameplay
    {
        public Vector2 Movement;
        public Vector2 Look;

        public int Interact;
    }

    // Assets
    public Gameplay gameplay;

    void Awake()
    {
        inputActions = new MainInputActions();

        inputActions.Gameplay.Enable();
        inputActions.UI.Enable();
    }

    void OnEnable()
    {
        inputActions.Gameplay.Enable();
        inputActions.UI.Enable();
    }

    void OnDisable()
    {
        inputActions.Gameplay.Disable();
        inputActions.UI.Disable();
    }

    // Start is called before the first frame update
    //void Start()
    //{
    //  
    //}

    // Update is called once per frame
    void Update()
    {
        // Update inputs for Update/Late Update functions
        #region Vector2 Inputs
        gameplay.Movement = inputActions.Gameplay.Movement.ReadValue<Vector2>();
        gameplay.Look = inputActions.Gameplay.Look.ReadValue<Vector2>();
        #endregion

        #region Buttons (Integer Inputs)
        bool buttonValue = inputActions.Gameplay.Interact.ReadValue<float>() >= .5f;
        gameplay.Interact = buttonValue ? (gameplay.Interact < 2 ? gameplay.Interact + 1 : 2) : 0;
        #endregion
    }
}