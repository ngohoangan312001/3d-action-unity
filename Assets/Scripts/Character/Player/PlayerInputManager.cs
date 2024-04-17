using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AN
{
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager instance;

        public PlayerManager player;
        
        PlayerControls playerControls;
        
        [Header("MOVEMENT INPUT")]
        [SerializeField] Vector2 movementInput;
        public float verticalInput;
        public float horizontalInput;
        public float moveAmount;
        
        [Header("CAMERA MOVEMENT INPUT")]
        [SerializeField] Vector2 cameraInput;
        public float cameraVerticalInput;
        public float cameraHorizontalInput;
        
        [Header("ACTION INPUT")]
        [SerializeField] bool rollInput = false;
        public bool jumpInput = false;
        public bool sprintInput = false;
        public bool aimInput = false;
        
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

            // subcribe OnSceneChange function to activeSceneChanged so => When scene change run OnSceneChange function
            SceneManager.activeSceneChanged += OnSceneChange;

            instance.enabled = false;    
        }

        private void OnSceneChange(Scene oldScene, Scene newScene)
        {
            if (newScene.buildIndex == WorldSaveGameManager.instance.GetWorldSceneIndex())
            {
                instance.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                instance.enabled = false;
            }
        }
        
        //OnEnable() được gọi trong các trường hợp sau:

        //Khi đối tượng được kích hoạt(enabled): Khi bật(hoặc kích hoạt) một đối tượng trong Unity(vd: bật một GameObject),
        //            hàm OnEnable() sẽ được thực thi.
        //=> đối tượng chuyển từ trạng thái không hoạt động sang trạng thái hoạt động.

        //Khi Component được kích hoạt (enabled): Nếu kích hoạt một Component (vd: script) trên một đối tượng,
        //            hàm OnEnable() của Component đó sẽ được gọi.
        //=> bật một Component sau khi đối tượng đã được khởi tạo.

        //OnEnable() thường được sử dụng để thực hiện các tác vụ liên quan đến việc khởi tạo và kích hoạt đối tượng trong Unity.
        //===>Thường được gọi sau hàm Awake() và trước hàm Start()

        private void OnEnable()
        {
            if(playerControls == null)
            {
                playerControls = new PlayerControls();

                // += operator asign function to the event
                playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
                playerControls.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();
                playerControls.PlayerAction.Roll.performed += i => rollInput = true;
                playerControls.PlayerAction.Sprint.performed += i => sprintInput = true;
                playerControls.PlayerAction.Sprint.canceled += i => sprintInput = false;
                playerControls.PlayerAction.Jump.performed += i => jumpInput = true;
                playerControls.PlayerAction.Aim.performed += i => aimInput = true;
                playerControls.PlayerAction.Aim.canceled += i => aimInput = false;
            }

            playerControls.Enable();
        }

        private void OnDestroy()
        {
            // if destroy this object, unsubcribe function from activeSceneChanged
            SceneManager.activeSceneChanged -= OnSceneChange;
        }

        private void Update()
        {
            HandleAllInput();
        }
        
        //if not active on application, disable control
        private void OnApplicationFocus(bool focus)
        {
            if (enabled)
            {
                if (focus)
                {
                    playerControls.Enable();
                }
                else
                {
                    playerControls.Disable();
                }
            }
        }

        private void HandleAllInput()
        {
            HandlePlayerMovementInput();
            HandleCameraMovementInput();
            HandlePlayerActionInput();
        }
        
        private void HandlePlayerActionInput()
        {
            HandleRollMovementInput();
            HandleSprintMovementInput();
            HandleJumpMovementInput();
        }
        
        private void HandlePlayerMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;

            //Clamp01: if value > 1 return 1 , if value < 0 return 0, else return value 
            moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

            // Move speed will only be 0, 0.5 or 1
            if(moveAmount <= 0.5 && moveAmount > 0)
            {
                moveAmount = 0.5f;
            }
            else if (moveAmount > 0.5 && moveAmount <= 1)
            {
                moveAmount = 1;
            }

            //not lock-on
            player.playerAnimatorManager.UpdateAnimatorMovementParameters(0 ,moveAmount);
        }

        private void HandleCameraMovementInput()
        {
            cameraVerticalInput = cameraInput.y;
            cameraHorizontalInput = cameraInput.x;
        }

        private void HandleRollMovementInput()
        {
            if (rollInput)
            {
                rollInput = false;
                
                //TODO: return (do nothing) if menu or UI window is open
                
                //perform a roll
                player.playerLocomotionManager.AttemptToPerformDodge();
            }   
        }
        
        private void HandleSprintMovementInput()
        {
            if (sprintInput)
            {
                player.playerLocomotionManager.HandleSprinting();
            }
            else
            {
                player.playerNetworkManager.isSprinting.Value = false;
            }
        }
        
        private void HandleJumpMovementInput()
        {
            if (jumpInput)
            {
                jumpInput = false;
                
                //TODO: return (do nothing) if menu or UI window is open
                
                //perform a jump
                player.playerLocomotionManager.HandleJumping();
            }
            else
            {
                player.playerNetworkManager.isJumping.Value = false;
            }
        }
    }
}
