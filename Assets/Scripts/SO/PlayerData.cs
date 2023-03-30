using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private int _maxJumps;
    [SerializeField] private int _damage;
    public int MaxHealth => _maxHealth;
    public float MoveSpeed => _moveSpeed;
    public float JumpForce => _jumpForce;
    public int MaxJumps => _maxJumps;
    public int Damage => _damage;


}