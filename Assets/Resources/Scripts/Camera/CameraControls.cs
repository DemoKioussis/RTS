using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraControls : MonoBehaviour
{
    public float movementSpeed = 0.1f;
    public float rotationSpeed = 4f;
    public float smoothness = 0.85f;
    public float scrollSpeedX = 1.0f;
    public float scrollSpeedY = 1.0f;
	public float offset = 50.0f;

    public AnimationCurve speedCurve;

    Vector3 targetPosition;
    
    public Quaternion targetRotation;
    float targetRotationY;
    float targetRotationX;

    Vector2 mousePosition;

	GameObject uiPanel;
	float uiHeight;

    // Use this for initialization
    void Start()
    {
        targetPosition = transform.position;
        targetRotation = transform.rotation;
        targetRotationY = transform.localRotation.eulerAngles.y;
        targetRotationX = transform.localRotation.eulerAngles.x;
		uiPanel = GameObject.FindGameObjectWithTag ("UIPanel");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if( Input.GetMouseButton( 1 ) )
        {
            Cursor.visible = false;
            targetRotationY += Input.GetAxis( "Mouse X" ) * rotationSpeed;
            targetRotationX -= Input.GetAxis( "Mouse Y" ) * rotationSpeed;
            targetRotation = Quaternion.Euler( targetRotationX, targetRotationY, 0.0f );
        }
        else
            Cursor.visible = true;

        transform.rotation = Quaternion.Lerp( transform.rotation, targetRotation, ( 1.0f - smoothness ) );

        mousePosition = Input.mousePosition;
		uiHeight = uiPanel.transform.position.y + uiPanel.GetComponent<RectTransform>().rect.size.y;

		/// MOVEMENT
		// Horizontal
		if (mousePosition.x >= (Screen.width - offset) || Input.GetKey(KeyCode.D))
			transform.Translate(Vector3.right * scrollSpeedX * Time.deltaTime, Space.World);
		else if (mousePosition.x <= (0.0f + offset) || Input.GetKey(KeyCode.A))
			transform.Translate(Vector3.left * scrollSpeedX * Time.deltaTime, Space.World);

		// Vertical
		if (mousePosition.y >= (Screen.height - offset) || Input.GetKey(KeyCode.W))
			transform.Translate(Vector3.forward * scrollSpeedY * Time.deltaTime, Space.World);
		else if (mousePosition.y <= (0.0f + offset) || Input.GetKey(KeyCode.S))
			transform.Translate(Vector3.back * scrollSpeedY * Time.deltaTime, Space.World);


    }

	bool IsInUI()
	{
		if (mousePosition.y < uiHeight)
			return true;

		return false;
	}
}
