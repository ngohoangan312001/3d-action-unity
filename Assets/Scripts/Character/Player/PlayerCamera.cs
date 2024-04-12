using System.Collections;
using System.Collections.Generic;
using AN;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCamera : MonoBehaviour
{
    [Header("Camera Settings")] 
    private float cameraSmoothSpeed = 1;
    [SerializeField] float leftAndRightRotationSpeed = 220;
    [SerializeField] float upAndDownRotationSpeed = 220;
    [SerializeField] float minimumPivot = -30;//Highest point can look up
    [SerializeField] float maximumPivot = 60;//Lowest point can look down
    [SerializeField] float cameraCollisionRadius = 0.2f;
    [SerializeField] LayerMask colliderWithLayers;
    
    [Header("Camera Values")] 
    private Vector3 cameraVelocity;
    private Vector3 cameraObjectPosition;// Use for camera collision (move camera to this posiotion con collision)
    [SerializeField] private float leftAndRightLookAngle;
    [SerializeField] private float upAndDownLookAngle;
    [SerializeField] private float cameraZPosition; //Value for camera collision
    [SerializeField] private float aimCameraZDistance = -0.5f;
    [SerializeField] private float aimCameraXDistance = 0.5f;
    private float targetCameraZPosition; //Value for camera collision
    private Vector3 aimVelocity;
    
    public static PlayerCamera instance;
    public Camera cameraObject;
    public PlayerManager player;

    [SerializeField] Transform cameraPivotTransform;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        cameraZPosition = cameraObject.transform.localPosition.z;
    }

    public void HandleAllCameraActions()
    {
        if (player != null)
        {
            //Aim camera
            HandleAimAction();
            //Collide with object
            HandleCollisions();
            //Follow
            HandleFollowTarget();
            //Rotate around player
            HandleRotations();
            
        }
        
    }

    private void HandleFollowTarget()
    {
        Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, player.transform.position,
            ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
        transform.position = targetCameraPosition;
    }
    
    private void HandleRotations()
    {
        //Lock on => force rotation toward target
        
        //===Normal rotation===
        
        //Get rotate left and right angle base on leftAndRightRotationSpeed
        leftAndRightLookAngle += PlayerInputManager.instance.cameraHorizontalInput * leftAndRightRotationSpeed * Time.deltaTime;
        
        //Get rotate up and down angle base on upAndDownRotationSpeed
        upAndDownLookAngle -= PlayerInputManager.instance.cameraVerticalInput * upAndDownRotationSpeed * Time.deltaTime;

        // Clamp value so it will between minimumPivot and maximumPivot
        upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minimumPivot, maximumPivot);

        Vector2 cameraRotation = Vector2.zero;
        Quaternion targetRotation;
        
        //Rotate left and right
        // Quaternion.Euler => Returns a rotation that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis.
        cameraRotation.y = leftAndRightLookAngle;
        targetRotation = Quaternion.Euler(cameraRotation);
        transform.rotation = targetRotation;

        //Rotate up and down
        cameraRotation = Vector2.zero;
        cameraRotation.x = upAndDownLookAngle;
        targetRotation = Quaternion.Euler(cameraRotation);
        cameraPivotTransform.localRotation = targetRotation;
    }

    private void HandleCollisions()
    {
        targetCameraZPosition = cameraZPosition;
        
        //Raycast là gì ==> raycast là một tia được gửi từ một vị trí trong không gian 3D hoặc 2D và di chuyển theo một hướng cụ thể.
        
        //Structure used to get information back from a raycast
        RaycastHit hit; 
        
        Vector3 direction = cameraObject.transform.position - cameraPivotTransform.position;
        direction.Normalize(); 
        
        // thực hiện một kiểm tra va chạm giữa một quả cầu (tại vị trí cameraPivotTransform.position) với bán kính cameraCollisionRadius và hướng direction.
        // Nếu có va chạm, kết quả sẽ được lưu trong biến hit.
        if(Physics.SphereCast(cameraPivotTransform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetCameraZPosition),colliderWithLayers))
        {
            // create a Sphere around camera z position that check for collision
            // then calculate and get the position at the collision point to move camera to that point
            float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.position, hit.point);
            targetCameraZPosition = -(distanceFromHitObject - cameraCollisionRadius);
        }

        if (Mathf.Abs(targetCameraZPosition) < cameraCollisionRadius)
        {
            targetCameraZPosition = -cameraZPosition;
        }
        cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, targetCameraZPosition,0.15f);
        
        // Camera Object will move on z-axis away or closer from it parrent because we use localPosition
        cameraObject.transform.localPosition = cameraObjectPosition;
    }

    private void HandleAimAction()
    {
        if (player.isPerformingAction)
        {
            player.playerNetworkManager.isAiming.Value = false;
            return;
        }
        
        if (PlayerInputManager.instance.aimInput)
        { 
            player.playerNetworkManager.isAiming.Value = true;
            cameraObjectPosition.z = aimCameraZDistance;
            cameraObjectPosition.x = aimCameraXDistance;
            cameraObject.transform.localPosition = cameraObjectPosition;
        }
        else
        {
            player.playerNetworkManager.isAiming.Value = false;
            cameraObjectPosition.x = 0;
        }
        
    }
}
