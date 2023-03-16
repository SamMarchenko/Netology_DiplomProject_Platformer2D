using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private int _maxJumps;
    public int MaxHealth => _maxHealth;
    public float MoveSpeed => _moveSpeed;
    public float JumpForce => _jumpForce;
    public int MaxJumps => _maxJumps;

    
}