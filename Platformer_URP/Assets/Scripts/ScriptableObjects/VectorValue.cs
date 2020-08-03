using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VectorValue : ScriptableObject, ISerializationCallbackReceiver {

    public Vector2 initialValue;
    public Vector2 defaultValue;
    public Vector2 runtimeValue;

    public void OnAfterDeserialize() {/* initialValue = defaultValue; */}

    public void OnBeforeSerialize(){}

}
