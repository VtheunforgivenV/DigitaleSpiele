using UnityEngine;
using System;

public interface Inventory {

    int GetID();
    Sprite GetImage();
    String GetName();
    String GetType();
    String GetDescription();
    int GetValue();
    void SetVisibility(bool visible);
    void SetID(int id);

}
