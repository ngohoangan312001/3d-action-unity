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

        PlayerControls playerControls;

        [SerializeField] Vector2 movementInput;
        public float verticalInput;
        public float horizontalInput;
        public float moveAmount;

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
            }
            else
            {
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
            HandleMovementInput();
        }

        private void HandleMovementInput()
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
            else if(moveAmount > 0.5 && moveAmount <= 1)
            {
                moveAmount = 1;
            }
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
    }
}
