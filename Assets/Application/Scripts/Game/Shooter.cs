﻿using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Thirties.Miniclip.TowerDefense
{
    public abstract class Shooter : MonoBehaviour
    {
        public UnityAction TargetFound { get; set; }

        public LayerMask TargetLayer { get { return targetLayer; } }
        public float Dps { get { return firePower / fireDelay; } }

        [Header("Parameters")]
        [Header("Hitscan parameters")]
        [SerializeField]
        protected float firePower = 5.0f;
        [SerializeField]
        private float fireDelay = 1.0f;
        [SerializeField]
        private float fireRange = 10.0f;
        public float FireRange { get { return fireRange; } }
        [SerializeField]
        private LayerMask targetLayer;

        [Header("References")]
        [SerializeField]
        private Transform rotatablePart;
        [SerializeField]
        protected Transform firingPoint;

        [Header("Particles")]
        [SerializeField]
        protected ParticleSystem fireParticles;

        [Header("SFX")]
        [SerializeField]
        protected RandomSFX randomSFX;

        protected Damageable currentTarget;
        private float elapsedTime = 0;
        private bool keepShooting = false;
        private bool keepLooking = false;

        protected void OnDestroy()
        {
            if (currentTarget != null)
            {
                currentTarget.Destroyed -= StopShooting;
            }
        }

        protected void Update()
        {
            elapsedTime += Time.deltaTime;
        }

        protected abstract void Shoot();

        public void StartLookingForTarget()
        {
            keepLooking = true;

            StartCoroutine(LookForTarget());
        }

        private void StartShootingTarget()
        {
            keepShooting = true;

            StartCoroutine(ShootTarget());
        }

        private IEnumerator LookForTarget()
        {
            while (keepLooking)
            {
                var hits = Physics.OverlapSphere(transform.position, fireRange, targetLayer);
                if (!hits.IsNullOrEmpty())
                {
                    currentTarget = hits
                        .OrderBy(x => Vector3.Distance(x.transform.position, transform.position))
                        .Select(x => x.transform.GetComponent<Damageable>())
                        .FirstOrDefault(x => x != null && x.IsAlive);

                    if (currentTarget != null)
                    {
                        keepLooking = false;

                        currentTarget.Destroyed += StopShooting;

                        TargetFound?.Invoke();
                    }
                }

                yield return null;
            }

            StartShootingTarget();
        }

        private IEnumerator ShootTarget()
        {
            //elapsedTime = fireDelay;

            while (keepShooting)
            {
                if (rotatablePart != null)
                {
                    rotatablePart.LookAt(currentTarget.transform);
                }

                if (elapsedTime >= fireDelay)
                {
                    if (fireParticles != null)
                    {
                        fireParticles.transform.position = firingPoint.position;
                        fireParticles.transform.LookAt(currentTarget.transform);
                        fireParticles.Play();
                    }

                    if (randomSFX != null)
                    {
                        randomSFX.PlayRandomClip();
                    }

                    Shoot();

                    elapsedTime %= fireDelay;
                }

                yield return null;

                //elapsedTime += Time.deltaTime;
            }

            StartLookingForTarget();
        }

        private void StopShooting()
        {
            keepShooting = false;
        }
    }
}
