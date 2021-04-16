﻿// 
// CanvasGroupManager.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.04.17
// 

using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Ling.Utility.UI
{
	public enum CanvasCameraType
	{
		None,	// 何もしない
		UI,		// UIカメラを設定する
	}

	/// <summary>
	/// CanvasGroupを一元管理する
	/// </summary>
	public class CanvasGroupManager : SerializedMonoBehaviour 
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] Dictionary<CanvasCameraType, Camera> _canvasCameraDict = default;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数
		
		/// <summary>
		/// CanvasGroupに値を適用する
		/// </summary>
		public void Apply(CanvasGroup canvasGroup)
		{

		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		void Awake()
		{
		}

		#endregion
	}
}