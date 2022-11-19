using Gummi;
using UnityEngine;

namespace Game.Player
{
    public class FPSController : MonoBehaviour
    {
        [SerializeField]
        Vector2 _lookSpeed = new(50, 50);

        [SerializeField]
        Vector2 _verticalBounds = new(-75, 75);

        [SerializeField]
        Transform _camera;

        [SerializeField, ReadOnly]
        float _cameraX;

        #region MonoBehaviour
        void OnEnable()
        {
            if (_camera == null)
            {
                Debug.LogError($"{ name } was not provided a camera to rotate.");
                this.enabled = false;
                return;
            }
            
            SetCursor(false);
        }

        void OnDisable()
        {
            SetCursor(true);
        }

        void Update() => UpdateRotation();
        #endregion

        static void SetCursor(bool enable)
        {
            Cursor.visible = enable;
            Cursor.lockState = enable ? CursorLockMode.None : CursorLockMode.Locked;
        }

        void UpdateRotation()
        {
            Vector2 look = GameManager.Instance.input.gameplay.Look;
            look *= _lookSpeed * Time.deltaTime;
            
            // apply horizontal rotation
            transform.Rotate(Vector3.up, look.x);

            // apply vertical rotation
            _cameraX = Mathf.Clamp(_cameraX - look.y, _verticalBounds.x, _verticalBounds.y);
            _camera.localRotation = Quaternion.Euler(_cameraX, 0, 0);
        }
    }
}