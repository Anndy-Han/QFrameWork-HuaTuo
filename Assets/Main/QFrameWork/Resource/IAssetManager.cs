﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace QFrameWork
{ 
    public interface IAssetManager 
    {
        T LoadAssetAsync<T>(string assetName) where T : UnityEngine.Object;
    }
}
