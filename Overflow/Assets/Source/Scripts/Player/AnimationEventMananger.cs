using UnityEngine;

public class AnimationEventMananger : MonoBehaviour
{
    [SerializeField] private PlayerAttack _playerAttack;
    public void Throw()
    {
        _playerAttack.SpawnCard();
    }
}
