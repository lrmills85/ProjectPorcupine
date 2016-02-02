using UnityEngine;
using System.Collections;

public class RTSCamera : MonoBehaviour
{
    public LayerMask floorLayer;

    [System.Serializable]
    public class PositionSettings
    {
        public bool invertPan = false;
        public float keyboardPanSmooth = 20f;
        public float mousePanSmooth = 7f;
        public float distanceFromGround = 40f;
        public bool allowZoom = true;
        public float zoomSmooth = 5f;
        public float zoomStep = 5f;
        public float maxZoom = 25;
        public float minZoom = 80f;

        [HideInInspector]
        public float newDistance = 40;
    }

    [System.Serializable]
    public class OrbitSettings
    {
        public float xRotation = 50f;
        public float yRotation = 0f;
        public bool allowYOrbit = true;
        public float yOrbitSmooth = 0.5f;
    }

    [System.Serializable]
    public class InputSettings
    {
        public string horizontalPan = "Horizontal";
        public string verticalPan = "Vertical";
        public string mousePan = "MousePan";
        public string Orbit_Y = "MouseTurn";
        public string zoom = "Mouse ScrollWheel";
    }

    public PositionSettings position = new PositionSettings();
    public OrbitSettings orbit = new OrbitSettings();
    public InputSettings input = new InputSettings();

    Vector3 destination = Vector3.zero;
    Vector3 camVel = Vector3.zero;
    Vector3 previousMousePos = Vector3.zero;
    Vector3 currentMousePos = Vector3.zero;
    float horizontalPanInput, verticalPanInput, mousePanInput, orbitInput, zoomInput; 
    int panDirection = 0;

    void Start()
    {
        horizontalPanInput = 0;
        verticalPanInput = 0;
        mousePanInput = 0;
        orbitInput = 0;
        zoomInput = 0;
    }

    void GetInput()
    {
        horizontalPanInput = Input.GetAxis(input.horizontalPan);
        verticalPanInput = Input.GetAxis(input.verticalPan);
        mousePanInput = Input.GetAxis(input.mousePan);
        orbitInput = Input.GetAxis(input.Orbit_Y);
        zoomInput = Input.GetAxis(input.zoom);

        previousMousePos = currentMousePos;
        currentMousePos = Input.mousePosition;
    }

    void Update()
    {
        //updating input
        GetInput();

        //zooming
        if (position.allowZoom)
            Zoom();

        //rotating
        if (orbit.allowYOrbit)
            Rotate();

        //panning
        PanWorld();
    }

    void FixedUpdate()
    {
        //handling camera distance
        HandleCameraDistance();
    }

    void PanWorld()
    {
        Vector3 targetPos = transform.position;

        if (position.invertPan)
            panDirection = -1;
        else
            panDirection = 1;

        if(mousePanInput > 0)
        {
            targetPos += transform.right * (currentMousePos.x - previousMousePos.x) * position.mousePanSmooth * panDirection * Time.deltaTime;
            targetPos += Vector3.Cross(transform.right, Vector3.up) * (currentMousePos.y - previousMousePos.y) * position.mousePanSmooth * panDirection * Time.deltaTime;
            transform.position = targetPos;
        }
        else
        {
            transform.Translate(new Vector3(horizontalPanInput, 0, 0) * position.keyboardPanSmooth * Time.deltaTime);
            transform.Translate(new Vector3(0, 0, verticalPanInput) * position.keyboardPanSmooth * Time.deltaTime, Space.World);
        }
    }

    void HandleCameraDistance()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100, floorLayer))
        {
            destination = Vector3.Normalize(transform.position - hit.point) * position.distanceFromGround;
            destination += hit.point;

            transform.position = Vector3.SmoothDamp(transform.position, destination, ref camVel, 0.3f);
        }

    }

    void Zoom()
    {
        position.newDistance += position.zoomStep * -zoomInput;
        position.distanceFromGround = Mathf.Lerp(position.distanceFromGround, position.newDistance, position.zoomSmooth * Time.deltaTime);

        if(position.distanceFromGround < position.maxZoom)
        {
            position.distanceFromGround = position.maxZoom;
            position.newDistance = position.maxZoom;
        }

        if(position.distanceFromGround > position.minZoom)
        {
            position.distanceFromGround = position.minZoom;
            position.newDistance = position.minZoom;
        }
    }

    void Rotate()
    {
        if(orbitInput > 0)
        {
            orbit.yRotation += (currentMousePos.x - previousMousePos.x) * orbit.yOrbitSmooth * Time.deltaTime;
        }

        transform.rotation = Quaternion.Euler(orbit.xRotation, orbit.yRotation, 0);
    }
}
