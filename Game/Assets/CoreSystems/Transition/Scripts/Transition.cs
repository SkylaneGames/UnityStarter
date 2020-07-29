using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreSystems.TransitionSystem
{
    [RequireComponent(typeof(Animator))]
    public class Transition : MonoBehaviour
    {
        public TransitionType TransitionType;
        public float TransitionTime = 1f;

        private Animator animator;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void TransitionOut()
        {
            animator.SetTrigger("Start");
        }
    }
}
