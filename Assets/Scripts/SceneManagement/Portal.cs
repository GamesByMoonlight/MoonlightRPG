using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        //In File>Build Settings, make sure to drag the scenes in the proper order
        //To give them the right scene index. That way, you can now load scenes based
        //On their index, incase you change the scene's name
        [SerializeField] int sceneToLoad = -1;




        ///Make an empty object, name it Portal
        ///Add a rigidbody and boxcolider component to it
        ///Remember to put this script into the portal game object
        ///And activate is Trigger in the box colider
        private void OnTriggerEnter(Collider other)
        {
           if(other.tag == "Player")
           {
               SceneManager.LoadScene(sceneToLoad);
           }
        }
    }
}