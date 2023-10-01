using UnityEngine;


public class CameraMovement : MonoBehaviour
{
    public Transform ObjToFollow
    {
        get => objToFollow;
        set
        {
            objToFollow = value;
            _deltaPos = transform.position - ObjToFollow.position;
        }
    }
    [SerializeField] private float _smoothPosition;
    private Vector3 _deltaPos;
    private Vector3 _currentVelocity = Vector3.zero;
    private Transform objToFollow;

    void Update()
    {
        if (ObjToFollow == null)
            return;
        Vector3 targetPosition = ObjToFollow.position + _deltaPos;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, _smoothPosition);
    }
}

