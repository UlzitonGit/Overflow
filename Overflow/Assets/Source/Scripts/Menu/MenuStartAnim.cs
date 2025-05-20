using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuStartAnim : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    private Coroutine _startEnum;
    public void StartGame()
    {
        if(_startEnum == null)
        _startEnum = StartCoroutine(Starting());
    }

    IEnumerator Starting()
    {
        _anim.SetTrigger("Start");
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(1);
    }
}
