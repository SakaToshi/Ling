﻿//
// SceneID.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.04.13
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Common.Scene
{
	public enum SceneID
	{
		None,
		StartUp, Main, Battle
	}

	/// <summary>
	/// Secne識別子
	/// </summary>
	public static class SceneIDExtensions
	{
		public static readonly Dictionary<SceneID, string> _sceneIDs = 
			new Dictionary<SceneID, string> 
				{
					[SceneID.StartUp] = "StartUp",
					[SceneID.Main] = "Main",
					[SceneID.Battle] = "Battle",
				};


		public static string GetName(this SceneID id) => 
			_sceneIDs[id];
	}
}
