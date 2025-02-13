using UnityEngine;

namespace PlayerFSM
{
    public abstract class PlayerState
    {
        protected readonly PlayerController player;

        protected PlayerState(PlayerController player)
        {
            this.player = player;
        }

        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void Exit() { }
    }
}