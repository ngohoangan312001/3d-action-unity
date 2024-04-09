using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
namespace AN
{
    public class CharacterManager : NetworkBehaviour
    {
        [HideInInspector] public CharacterController characterController;
        [HideInInspector] public Animator animator;
        [HideInInspector] public CharacterNetworkManager characterNetworkManager;

        [Header("Flag")] 
        public bool isPerformingAction = false;
        public bool applyRootMotion = false;
        public bool canRotate = true;
        public bool canMove = true;
        
        protected virtual void Awake()
        {
            DontDestroyOnLoad(this);

            characterController = GetComponent<CharacterController>();

            characterNetworkManager = GetComponent<CharacterNetworkManager>();
            
            animator = GetComponent<Animator>();
        }

        protected virtual void Update()
        {

            // if character being controlled by owner, then assign posiotion to it network position to make it move on other client
            if (IsOwner)
            {
                characterNetworkManager.networkPosition.Value = transform.position;
                characterNetworkManager.networkRotation.Value = transform.rotation;
            }
            //if character is being controller by other, get it network position and update it position on our client
            else
            { 
                
            //=====Position=====
            //Hàm Vector3.SmoothDamp trong Unity được sử dụng để mượt dần một vector về một mục tiêu mong muốn theo thời gian.
            //Thường được sử dụng để làm cho một vector di chuyển mượt mà, vd như trong trường hợp của một camera theo dõi

            //Parameter
            //current: Vector hiện tại bạn muốn mượt dần.
            //target: Vector mục tiêu bạn muốn đạt được.
            //currentVelocity: Tham chiếu đến vector tốc độ hiện tại(sẽ được cập nhật bởi hàm).
            //smoothTime: Thời gian mượt dần(thường là giá trị dương).
            //maxSpeed(tùy chọn): Tốc độ tối đa(mặc định là vô cùng).
            //deltaTime(tùy chọn): Khoảng thời gian giữa các khung hình(mặc định là Time.deltaTime).
               transform.position = Vector3.SmoothDamp(
                    transform.position,
                    characterNetworkManager.networkPosition.Value,
                    ref characterNetworkManager.networkPositionVelocity,
                    characterNetworkManager.networkPositionSmoothTime);
               
               
            //=====Rotation=====
               
            // Quaternion.Slerp (short for Spherical Linear Interpolation) là một phương thức trong Unity
            // được sử dụng để mượt dần giữa hai Quaternion theo một đường cung trên bề mặt của một quả cầu.
            // Thường được sử dụng để tạo ra chuyển động mượt mà giữa hai hướng quay trong không gian 3D .   
               transform.rotation = Quaternion.Slerp(
                   transform.rotation,
                   characterNetworkManager.networkRotation.Value,
                   characterNetworkManager.networkRotationSmoothTime);
            }
            
        }

        protected virtual void LateUpdate()
        {
        }
    }
}
