using System;
using UnityEngine;
using YIUIFramework;
using System.Collections.Generic;

namespace ET.Client
{
    /// <summary>
    /// Author  OneQi
    /// Date    2024.9.9
    /// Desc
    /// </summary>
    public partial class ComfyUIViewComponent: Entity
    {
        public string selectFileDir = "Select File Directory...";
        public string pattern = @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}:\d{1,5}$";
    }
}