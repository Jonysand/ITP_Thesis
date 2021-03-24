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
    [SerializeField] PlayerSingleton.DailyState[] validStates;

    void Start()
    {
        IsInteractable = false;
        foreach (var valid_state in validStates)
        {
            if (PlayerSingleton.instance.state == valid_state)
            {
                IsInteractable = true;
            }
        }
    }

    public override void OnInteract()
    {
        base.OnInteract();
        StartCoroutine(LoadLevel(exitScene));
    }

    IEnumerator LoadLevel(string sceneName)
    {
        SceneTransAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(TransitionTime);
        PlayerSingleton.instance.state = PlayerSingleton.nextState(PlayerSingleton.instance.state);
        SceneManager.LoadScene(exitScene, LoadSceneMode.Single);
    }
}
