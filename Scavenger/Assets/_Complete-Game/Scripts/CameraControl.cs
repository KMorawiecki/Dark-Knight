using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float m_DampTime = 0.2f;                 // Approximate time for the camera to refocus.
    //public float m_ScreenEdgeBuffer = 4f;           // Space between the top/bottom most target and the screen edge.
    //public float m_MinSize = 6.5f;                  // The smallest orthographic size the camera can be.
    public Transform m_Target;                      // Targets the camera needs to encompass.


    private Camera m_Camera;                        // Used for referencing the camera.
    private Vector3 m_MoveVelocity;                 // Reference velocity for the smooth damping of the position.
    private Vector3 m_DesiredPosition;              // The position the camera is moving towards.


    private void Awake()
    {
        m_Camera = GetComponentInChildren<Camera>();
    }


    private void FixedUpdate()
    {
        // Move the camera towards a desired position.
        Move();
    }


    private void Move()
    {
        // Find the position of the targets.
        FindPosition();

        // Smoothly transition to that position.
        transform.position = Vector3.SmoothDamp(transform.position, m_DesiredPosition, ref m_MoveVelocity, m_DampTime);
    }


    private void FindPosition()
    {
        // The desired position
        m_DesiredPosition = new Vector3(m_Target.position.x, m_Target.position.y, transform.position.z);
    }


    public void SetStartPositionAndSize()
    {
        // Find the desired position.
        FindPosition();

        // Set the camera's position to the desired position without damping.
        transform.position = m_DesiredPosition;
    }
}
