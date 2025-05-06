using UnityEngine;

public interface IPlayable
{
   
    public PlayerController PlayerControllerBind { get; set; }
    public void InitializePlayer();
    public Transform PlayerTransform();
    public void IsActive(bool isActive);
}
