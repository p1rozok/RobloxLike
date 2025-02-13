namespace PlayerFSM
{
    public class RunState : PlayerState
    {
        public RunState(PlayerController player) : base(player) { }

        public override void Enter()
        {
            player.Animator.Play("Run");
        }

        public override void Update()
        {
            player.Move();

            if (!player.IsMoving())
            {
                player.ChangeState(new IdleState(player));
            }

            if (player.IsJumpPressed())
            {
                player.ChangeState(new JumpState(player));
            }
        }
    }
}