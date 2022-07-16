using System;
using System.Collections;
using Events;
using GlobalConstants;
using UniRx;
using UnityEngine;

namespace Core.RoadFollower
{
    public class PlayerRoadFollower: BaseRoadFollower
    {
        [SerializeField]
        private float _accelerationPerSecond = 3f;
        [SerializeField]
        private float _timeToStop = 1f;
        [SerializeField]
        private float _startDistanceOffset;
        
        [Space]
        [Header("Effects")]
        [SerializeField]
        private ParticleSystem _smokeEffect;
        
        private Coroutine _increaseSpeedCoroutine;
        private Coroutine _decreaseSpeedCoroutine;

        public override void Awake()
        {
            base.Awake();

            TravelledDistance.Value += _startDistanceOffset;
            
            Speed.AsObservable().Subscribe(_ =>
            {
                if (_ > 0)
                {
                    _smokeEffect.Play();
                }
                else
                {
                    _smokeEffect.Stop();
                }
            }).AddTo(this);
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartMoving();
            }
            
            if (Input.GetKeyUp(KeyCode.Space))
            {
                Stop();
            }    
#endif
            
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    StartMoving();
                }
                
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    Stop();
                }
            }

            base.Move();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(Tags.NPC))
            {
                var collisionVector = other.contacts[0].normal;
                EventStreams.UserInterface.Publish(new PlayerCrashedEvent(collisionVector));
            }
        }

        protected override void Stop()
        {
            if(_increaseSpeedCoroutine != null)
            {
                StopCoroutine(_increaseSpeedCoroutine);
            }
            
            StartCoroutine(DecreaseSpeed());
        }

        private void StartMoving()
        {
            if (_decreaseSpeedCoroutine != null)
            {
                StopCoroutine(_decreaseSpeedCoroutine);
            }
            _increaseSpeedCoroutine = StartCoroutine(IncreaseSpeed());
        }
        
        private IEnumerator IncreaseSpeed()
        {
            while (Speed.Value <= _maxSpeed)
            {
                Speed.Value += _accelerationPerSecond * Time.deltaTime;
                yield return null;
            }
        }
        
        private IEnumerator DecreaseSpeed()
        {
            var startTime = Time.time;
            var startSpeed = Speed.Value;
            while (Time.time - startTime < _timeToStop)
            {
                Speed.Value = Mathf.Lerp(startSpeed, 0, (Time.time - startTime) / _timeToStop);
                yield return null;
            }
            
            Speed.Value = 0;
        }
    }
}
