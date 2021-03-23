using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class InteractableSceneTrans : InteractableBase
{
    [SerializeField] string exitScene;
    [SerializeField] Animator SceneTransAnimator;
    [SerializeField] float TransitionTime = 1.0f;

    public override void OnInteract()
    {
        base.OnInteract();
        StartCoroutine(LoadLevel(exitScene));
    }

    IEnumerator LoadLevel(string sceneName)
    {
        SceneTransAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(TransitionTime);
        SceneManager.LoadScene(exitScene, LoadSceneMode.Single);
    }
}
