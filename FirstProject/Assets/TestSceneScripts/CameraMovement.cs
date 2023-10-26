using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float smoothTime = 1f;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] GameObject camera;

    public float speed = 2;
    void Update()
    {
        Transform camTransform = camera.transform;
        Vector3 camPosition = new Vector3(camTransform.position.x, transform.position.y, camTransform.position.z);

        Vector3 direction = (transform.position - camPosition).normalized;

        Vector3 forwardMovement = direction * Input.GetAxis("Vertical");
        Vector3 horizontalMovement = camTransform.right * Input.GetAxis("Horizontal");

        Vector3 movement = Vector3.ClampMagnitude(forwardMovement + horizontalMovement, 1);

        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
    //void LateUpdate()
    //{
    //    transform.LookAt(target);
    //    if (Vector3.Distance(transform.position, target.position) > 7f)
    //    {
    //        Debug.Log("Moving");
    //        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smoothTime);
    //    }
    //}
}
