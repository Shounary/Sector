using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobileEntity : Entity
{
    public NavMeshAgent moveAgent;
    public string ownerName = "";

    public string getOwnership() {
        return ownerName;
    }

    public NavMeshAgent getMoveAgent() {
        return moveAgent;
    }
}
