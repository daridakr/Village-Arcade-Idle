using UnityEngine;

namespace Village.Character
{
    public class CharacterTouchRotator : MonoBehaviour
    {
        private bool _isRotating = false;
        private Vector2 _startPosition;

        private const float _speed = 1f;

        void Update()
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                _startPosition = Input.mousePosition;
                _isRotating = true;
            }

            if (_isRotating && Input.GetMouseButton(0))
            {
                float touchDeltaX = Input.mousePosition.x - _startPosition.x;
                Rotate(touchDeltaX);
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isRotating = false;
            }
#else
            if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                _startPosition = touch.position;
                _isRotating = true;
            }

            if (_isRotating && touch.phase == TouchPhase.Moved)
            {
                float touchDeltaX = touch.position.x - _startPosition.x;
                Rotate(touchDeltaX);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                _isRotating = false;
            }
        }
#endif
        }

        private void Rotate(float angle)
        {
            float rotationAmount = angle * _speed * Time.deltaTime;
            transform.Rotate(Vector3.up, rotationAmount);
        }
    }
}