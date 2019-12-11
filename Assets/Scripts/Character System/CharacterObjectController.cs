using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterTypes
{
    player,
    enemy
}

public abstract class CharacterObjectController : MonoBehaviour
{
    [SerializeField] private GeneratorManager generator;
    [SerializeField] protected CharacterTypes _type;
    [SerializeField] protected Weapon weapon;
    protected ObjectMovement movement;
    protected CharacterStats _stats;
    protected CombatController combatController;
    protected Chunk _currentChunk;

    public CharacterStats stats { get => _stats; }
    public Chunk currentChunk => _currentChunk;
    public CharacterTypes type { get => _type; }
    public GeneratorManager Generator { get => generator; set => generator = value; }
   

    public abstract void OnUpdate();
    protected abstract void OnDead();

    protected virtual void Awake()
    {
        _stats = GetComponent<CharacterStats>();
        combatController = GetComponent<CombatController>();
    }

    protected Chunk CheckOnWhichChunkYouStayed(Vector2 position)
    {
        return Generator.getChunkTransform(GridCellCalculator.ComputeChunkPosition(position)).GetComponent<Chunk>();
    }
}
