using System;
using UnityEngine;

namespace ET.Client
{
    public class Collider2DCallback : MonoBehaviour
    {
        public Action<Collision2D> OnCollisionEnter2DAction;
        public Action<Collision2D> OnCollisionStay2DAction;
        public Action<Collision2D> OnCollisionExit2DAction;

        public Action<Collider2D> OnTriggerEnter2DAction;
        public Action<Collider2D> OnTriggerStay2DAction;
        public Action<Collider2D> OnTriggerExit2DAction;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEnter2DAction?.Invoke(collision);
        }
        private void OnCollisionStay2D(Collision2D collision)
        {
            OnCollisionStay2DAction?.Invoke(collision);
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            OnCollisionExit2DAction?.Invoke(collision);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnTriggerEnter2DAction?.Invoke(other);
        }
        private void OnTriggerStay2D(Collider2D other)
        {
            OnTriggerStay2DAction?.Invoke(other);
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            OnTriggerExit2DAction?.Invoke(other);
        }
    }
}