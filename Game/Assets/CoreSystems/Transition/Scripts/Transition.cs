using UnityEngine;
using UnityEngine.Serialization;

namespace CoreSystems.Transition.Scripts
{
    [RequireComponent(typeof(Animator))]
    public class Transition : MonoBehaviour
    {
        [FormerlySerializedAs("TransitionType")] public TransitionType transitionType;
        [FormerlySerializedAs("TransitionTime")] public float transitionTime = 1f;

        private Animator _animator;
        private static readonly int Start = Animator.StringToHash("Start");

        void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void TransitionOut()
        {
            _animator.SetTrigger(Start);
        }
    }
}
