using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {

        enum DestinationIdentifier
        {
            A, B, C, D, E
        }

        //In File>Build Settings, make sure to drag the scenes in the proper order
        //To give them the right scene index. That way, you can now load scenes based
        //On their index, incase you change the scene's name
        [SerializeField] int sceneToLoad = -1;

        [SerializeField] Transform spawnPoint;

        [SerializeField] DestinationIdentifier destination;




        ///Make an empty object, name it Portal
        ///Add a rigidbody and boxcolider component to it
        ///Remember to put this script into the portal game object
        ///And activate is Trigger in the box colider
        private void OnTriggerEnter(Collider other)
        {
           if(other.tag == "Player")
           {
               StartCoroutine(Transition());
           }
        }

        ///Using a Coroutine and IEnummerator to keep the information we had at the start
        ///Allowing us to keep information for when we start needing the character stats, equip, etc.
        private IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            Destroy(gameObject);
        }

        ///This piece is for placing the player in the proper position
        ///Otherwise you would load the scene in whatever position the player
        ///Is already set in in the scene set up
        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            //Note the warp was a point they added specifically in section 72
            //That isn't shown throughout the tutorial. It is for having your spawn point
            //Acurate without it interfering with the NavMeshAgent, which also Updates the Player position
            //Rotation is not affected
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            foreach(Portal portal in FindObjectsOfType<Portal>())
            {
                if(portal == this) continue;
                if(portal.destination != destination) continue;

                return portal;
            }
            return null;
        }
    }
}