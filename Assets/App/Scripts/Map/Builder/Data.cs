﻿//
// Data.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.12.23
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Map.Builder
{
	/// <summary>
	/// 
	/// </summary>
    public class Data
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private int _roomMinSize = 3;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 部屋の最小サイズ
		/// </summary>
		public int RoomMinSize => _roomMinSize;

		#endregion


		#region private 関数

		#endregion
	}
}
