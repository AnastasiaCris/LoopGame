using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Transitions : MonoBehaviour
{
    #region Variables
    [Header("Transition Animations")]
    [SerializeField] private Animator simpleSceneTransition;
    [SerializeField] private Animator simpleTransition;
    [Tooltip("the time it takes for the scene to change after the transition animation started")]
    [SerializeField] private float sceneTransitionTime = 1f;

    #endregion

    #region Functions

    /// <summary>
    /// Starts a scene change with transition
    /// </summary>
    /// <param name="name"> Tell the name of the scene </param>
    public void LoadSimpleSceneTransition(string name)
    {
        StartCoroutine(LoadScene(name));
    }

    public void StartSimpleTransition()
    {
        StartCoroutine(StartTransition());
    }

    #endregion

    #region Coroutines

    /// <summary>
    /// Switches scenes with transitions
    /// </summary>
    /// <param name="sceneName">Tells the name of the scene to switch to</param> 
    /// <returns></returns>
    IEnumerator LoadScene(string sceneName)
    {
        yield return new WaitForSeconds(1f);

        simpleSceneTransition.SetTrigger("Start");

        yield return new WaitForSeconds(sceneTransitionTime);
        Scenes.previousScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator StartTransition()
    {
        yield return new WaitForSeconds(1f);
        simpleTransition.SetTrigger("Start");
    }
    #endregion
}
