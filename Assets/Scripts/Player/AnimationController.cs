using UnityEngine;

namespace DefaultNamespace.Player
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