﻿//
// TileData.cs
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


namespace Ling.Map
{
	/// <summary>
	/// マップの中の一マスのデータ
	/// </summary>
	public struct TileData
	{
		/// <summary>
		/// タイルデータがもつフラグ
		/// </summary>
		public TileFlag Flag { get; private set; }

		public Vector2Int Pos { get; private set; }
		public int X => Pos.x;
		public int Y => Pos.y;


		public int Index { get; private set; }

		/// <summary>
		/// 壁ならtrue
		/// </summary>
		public bool IsWall => HasFlag(TileFlag.Wall);

		/// <summary>
		/// 上階段ならtrue
		/// </summary>
		public bool IsStepUp => HasFlag(TileFlag.StepUp);


		/// <summary>
		/// 初期化
		/// </summary>
		public void Initialize()
		{
			Flag = TileFlag.None;
		}

		public void SetPos(int x, int y) 
		{
			Pos = new Vector2Int(x, y);
		}
		public void SetIndex(int index) => Index = index;

		/// <summary>
		/// フラグとして情報を追加する
		/// </summary>
		/// <param name="tileFlag"></param>
		public void AddFlag(TileFlag tileFlag)
		{
			Flag |= tileFlag;
		}

		public void SetFlag(TileFlag tileFlag)
		{
			Flag = tileFlag;
		}

		/// <summary>
		/// フラグを削除する
		/// </summary>
		/// <param name="tileFlag"></param>
		public void RemoveFlag(TileFlag tileFlag)
		{
			Flag &= ~tileFlag;
		}

		/// <summary>
		/// 指定したフラグを持っているか
		/// </summary>
		/// <returns></returns>
		public bool HasFlag(TileFlag tileFlag)
		{
			// enum のHasFlagは引数のflagをどちらとも持っていないと0を返すので注意
			//return Flag.HasFlag(tileFlag);
			return (tileFlag & Flag) != 0;
		}


		/// <summary>
		/// 壁にする
		/// </summary>
		public void SetWall()
		{
			// 壁にするときに初期化する
			Initialize();

			AddFlag(TileFlag.Wall);
		}

	}
}