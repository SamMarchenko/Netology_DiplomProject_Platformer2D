using UnityEngine;

namespace DefaultNamespace.Players
{
    public class AnimationController
    {
        private Animator _animator;

        public AnimationController(Animator animator)
        {
            _animator = animator;
        }

        public void PlayAnimation(EAnimStates state)
        {
            _animator.SetInteger("State", (int)state);
        }
    }
}