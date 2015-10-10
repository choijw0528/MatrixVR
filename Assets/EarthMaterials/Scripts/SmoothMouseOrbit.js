var target : Transform;
var distance = 10.0;

// ZoomCameraMouse
var MouseWheelSensitivity = 5;
var MouseZoomMin = 1;
var MouseZoomMax = 7;

var xSpeed = 250.0;
var ySpeed = 120.0;

var yMinLimit = -20;
var yMaxLimit = 80;

private var x = 0.0;
private var y = 0.0;


var smoothTime = 0.3;

private var xSmooth = 0.0;
private var ySmooth = 0.0; 
private var xVelocity = 0.0;
private var yVelocity = 0.0;

private var posSmooth = Vector3.zero;
private var posVelocity = Vector3.zero;

function Start () {
   
    Screen.showCursor = true;

    var angles = transform.eulerAngles;
    x = angles.y;
    y = angles.x;

    // Make the rigid body not change rotation
    if (rigidbody)
        rigidbody.freezeRotation = true;
}

function LateUpdate () {

    if (target && Input.GetAxis("Fire3")) {
        x += Input.GetAxis("Mouse X") * xSpeed * 0.02;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02;

        xSmooth = Mathf.SmoothDamp(xSmooth, x, xVelocity, smoothTime);
        ySmooth = Mathf.SmoothDamp(ySmooth, y, yVelocity, smoothTime);

        ySmooth = ClampAngle(ySmooth, yMinLimit, yMaxLimit);

        rotation = Quaternion.Euler(ySmooth, xSmooth, 0);

       // posSmooth = Vector3.SmoothDamp(posSmooth,target.position,posVelocity,smoothTime);

        posSmooth = target.position; // no follow smoothing

        transform.rotation = Quaternion.Euler(ySmooth, xSmooth, 0);
        transform.position = transform.rotation * Vector3(0.0, 0.0, -distance) + posSmooth;
    }
    
	if (Input.GetAxis("Mouse ScrollWheel") != 0) {

    if (distance >= MouseZoomMin && distance <= MouseZoomMax){

    distance -= Input.GetAxis("Mouse ScrollWheel") * MouseWheelSensitivity;

    if (distance < MouseZoomMin){distance = MouseZoomMin;}
    if (distance > MouseZoomMax){distance = MouseZoomMax;}
   }
   }	
   
   transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(y, x, 0), Time.deltaTime * 3);
   transform.position = transform.rotation * Vector3(0.0, 0.0, -distance) + target.position;   
   
}

static function ClampAngle (angle : float, min : float, max : float) {
    if (angle < -360)
        angle += 360;
    if (angle > 360)
        angle -= 360;
    return Mathf.Clamp (angle, min, max);
}