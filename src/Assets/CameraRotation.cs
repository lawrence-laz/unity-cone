using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float RotateSpeed = 6;
    public float ZoomSpeed = 5;
    public Transform Target;

    private void Update()
    {
        if (Input.GetMouseButton(0)) 
        { 
            transform.RotateAround(Target.position, Target.up, Input.GetAxis("Mouse X") * RotateSpeed); 
            transform.RotateAround(Target.position, transform.right, Input.GetAxis("Mouse Y") * -RotateSpeed); 
        }

        transform.position += (transform.position - Target.position).normalized * ZoomSpeed * Input.mouseScrollDelta.y * Time.deltaTime;
    }
}
