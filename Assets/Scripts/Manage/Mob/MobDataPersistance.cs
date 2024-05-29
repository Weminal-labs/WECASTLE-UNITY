using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MobDataPersistance
{
    void LoadData(MobStats data);
    bool CompareData(string id);
    void SaveData(ref MobStats data);
}
