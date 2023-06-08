using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IADesicion : ScriptableObject
{
    public abstract bool Decidir(IAController controller);
}
