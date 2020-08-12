using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class YellowController : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }         
        public ThirdPersonCharacter character { get; private set; } 
        public Transform target;
        public Transform targeteach;
        private Animator anim;

        public static bool yellowchosen = false;
        public static bool bluechosen = false;
        public static bool redchosen = true;
        public static bool yellowpaused = false;
        public bool letClickRed = false;
        public bool letClickBlue = false;

        private void Start()
        {
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();
            anim = GetComponent<Animator>();

            agent.updateRotation = false;
            agent.updatePosition = true;
        }


        private void Update()
        {
            yellowpaused = PlayerController.yellowpaused;
            if (!yellowpaused)
            {
                if (yellowchosen)
                {
                    agent.SetDestination(PlayerController.character.position);

                    if (agent.remainingDistance > agent.stoppingDistance)
                    {
                        character.Move(agent.desiredVelocity, false, false);
                        anim.SetBool("Walk", true);
                    }
                    else
                    {
                        character.Move(Vector3.zero, false, false);
                        anim.SetBool("Walk", false);
                    }
                }
                else
                {
                    agent.SetDestination(target.position);

                    if (agent.remainingDistance > agent.stoppingDistance)
                    {
                        character.Move(agent.desiredVelocity, false, false);
                        anim.SetBool("Walk", true);
                    }
                    else
                    {
                        character.Move(Vector3.zero, false, false);
                        anim.SetBool("Walk", false);
                    }
                }
                Catching();
            }
            else anim.SetTrigger("Stop");
        }

        void OnCollisionStay(Collision col)
        {
            if (col.transform.tag == "CatchRed") letClickRed = true;
            else letClickRed = false;
            if (col.transform.tag == "CatchBlue") letClickBlue = true;
            else letClickBlue = false;           
        }

        void Catching()
        {
            yellowchosen = PlayerController.yellowchosen;
            if (letClickRed && yellowchosen)
            {
                redchosen = true;
                PlayerController.chosenone = true;
                yellowchosen = false;
                PlayerController.yellowchosen = false;
            }
            if (letClickBlue && yellowchosen)
            {
                bluechosen = true;
                PlayerController.bluechosen = true;
                yellowchosen = false;
                PlayerController.yellowchosen = false;
            }
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
