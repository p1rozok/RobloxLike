namespace PlayerFSM
{
    public class IdleState : PlayerState
    {
        public IdleState(PlayerController player) : base(player) { }

        public override void Enter()
        {
            player.Animator.Play("Idle");
        }

        public override void Update()
        {
            if (player.IsMoving())
            {
                player.ChangeState(new RunState(player));
            }

            if (player.IsJumpPressed())
            {
                player.ChangeState(new JumpState(player));
            }
        }
    }
}