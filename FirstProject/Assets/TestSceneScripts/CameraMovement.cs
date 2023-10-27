using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //[SerializeField] Transform target;
    //[SerializeField] GameObject camera;

    public Transform player;
    public float cameraDistance = 5;
    public float cameraHeight = 3;
    public float smoothTime = 0.5f;
    [SerializeField] private float speed = 10;
    Vector3 velocity;
    void Update()
    {
        //Transform camTransform = camera.transform;
        //Vector3 camPosition = new Vector3(camTransform.position.x, transform.position.y, camTransform.position.z);

        //Vector3 direction = (transform.position - camPosition).normalized;

        //Vector3 forwardMovement = direction * Input.GetAxis("Vertical");
        //Vector3 horizontalMovement = camTransform.right * Input.GetAxis("Horizontal");

        //Vector3 movement = Vector3.ClampMagnitude(forwardMovement + horizontalMovement, 1);

        //transform.Translate(movement * speed * Time.deltaTime, Space.World);



        transform.LookAt(player.transform);
        Vector3 offset = (transform.position - player.position).normalized * cameraDistance;
        Vector3 targetPosition = player.position + offset;
        targetPosition.y = cameraHeight;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, speed);

        if (Vector3.Distance(transform.position, player.position) < 4.5f)
        {
            speed = 30;
            smoothTime = 0.2f;
        }
        else
        {
            speed = 10;
            smoothTime = 0.5f;
        }
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
