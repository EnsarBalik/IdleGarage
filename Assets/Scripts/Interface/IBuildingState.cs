using UnityEngine;

public interface IBuildingState
{
    public void EndState();
    void OnAction(Vector3Int gridPosition);
    void UpdateState(Vector3Int gridPosition);
}
