using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AN
{
    public class AICharacterManager : CharacterManager
    {
        public AICharacterCombatManager aiCharacterCombatManager;
        [Header("Current State")] 
        [SerializeField] private AIState currentState;

        protected override void Awake()
        {
            base.Awake();

            aiCharacterCombatManager = GetComponent<AICharacterCombatManager>();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            ProcessStateMachine();
        }

        public void ProcessStateMachine()
        {
            AIState nextState = currentState?.Tick(this);

            if (nextState != null)
            {
                currentState = nextState;
            }
        }
    }
}
