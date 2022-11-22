using System;
using UnityEngine;

public enum InteractionType
{
    None = 0, Down = 1, Hold = 2, Up = 3,
}

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
        public Vector2 MouseDelta;
        
        public float Scroll;
        public float Rotate;

        public InteractionType Grab;
        public InteractionType Throw;
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

    void Update()
    {
        // Update inputs for Update/Late Update functions
        #region Vector2 Inputs
        gameplay.Movement = inputActions.Gameplay.Movement.ReadValue<Vector2>();
        gameplay.Look = inputActions.Gameplay.Look.ReadValue<Vector2>();
        gameplay.MouseDelta = inputActions.Gameplay.MouseDelta.ReadValue<Vector2>();
        #endregion
        
        #region float Inputs
        gameplay.Scroll = inputActions.Gameplay.Scroll.ReadValue<float>();
        gameplay.Rotate = inputActions.Gameplay.Rotate.ReadValue<float>();
        #endregion

        #region Buttons (Integer Inputs)
        bool buttonValue = inputActions.Gameplay.Grab.ReadValue<float>() >= .5f;
        HandleButton(buttonValue, ref gameplay.Grab);
        
        buttonValue = inputActions.Gameplay.Throw.ReadValue<float>() >= .5f;
        HandleButton(buttonValue, ref gameplay.Throw);
        #endregion
    }

    // cycle through button input values
    static void HandleButton(bool value, ref InteractionType output)
    {
        switch (output)
        {
            case InteractionType.None:
                output = value ? InteractionType.Down : InteractionType.None;
                return;
            
            case InteractionType.Down:
            case InteractionType.Hold:
                output = value ? InteractionType.Hold : InteractionType.Up;
                return;
            
            case InteractionType.Up:
                output = value ? InteractionType.Down : InteractionType.None;
                break;
                
            default:
                throw new ArgumentOutOfRangeException(nameof(output), output, null);
        }
    }
}