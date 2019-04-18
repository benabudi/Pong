using UnityEngine;

public class Player : MonoBehaviour
{
    
    public enum PlayerType
    {
        Left,
        Right
    }

    #region Editor exposed members
    [SerializeField] private PlayerType _playerType;
    [SerializeField] private float _movementSpeed = 5;
    #endregion

    #region Private members
    private Transform _transform;
    private float _halfHeight;
    #endregion

    private void Start()
    {
        
        _transform = transform;
        _halfHeight = GetComponent<Collider>().bounds.extents.y;
    }

    private void Update()
    {
        

        float max = ScreenUtil.ScreenPhysicalBounds.yMax;
        float min = ScreenUtil.ScreenPhysicalBounds.yMin;

        bool moveUp = _transform.position.y + _halfHeight < max;
        bool moveDown = _transform.position.y - _halfHeight > min;

        if (_playerType == PlayerType.Left)
        {
            if (Input.GetKey(KeyCode.W) && moveUp)
            {
                _transform.position += _movementSpeed * Vector3.up * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S) && moveDown)
            {
                _transform.position -= _movementSpeed * Vector3.up * Time.deltaTime;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow) && moveUp)
            {
                _transform.position += _movementSpeed * Vector3.up * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.DownArrow) && moveDown)
            {
                _transform.position -= _movementSpeed * Vector3.up * Time.deltaTime;
            }
        }

        
    }
}