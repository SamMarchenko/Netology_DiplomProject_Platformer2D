using UnityEngine;

namespace DefaultNamespace.Players
{
    public class AnimationController
    {
        private Animator _animator;

        public void Init(Animator animator)
        {
            _animator = animator;
        }
        
        public void PlayAnimation(EAnimStates state)
        {
            _animator.SetInteger("State", (int)state);
        }
    }
}