using UnityEngine;

namespace PlayerFSM
{
    public class JumpState : PlayerState
    {
        private float jumpTimer = 0f;
        private float jumpDuration = 1.0f; 

        public JumpState(PlayerController player) : base(player) { }

        

        public override void Update()
        {
            jumpTimer += Time.deltaTime;
            if (jumpTimer >= jumpDuration)
            {
                if (player.IsMoving())
                    player.ChangeState(new RunState(player));
                else
                    player.ChangeState(new IdleState(player));
            }
        }
        
        public override void Enter()
                 {
                     player.Animator.SetTrigger("Jump");
                     player.Jump();
                     jumpTimer = 0f;
                 }
    }
}