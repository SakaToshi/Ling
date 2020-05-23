﻿//
// MapData.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.04.21
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Map
{
	/// <summary>
	/// <see cref="TileData"/>を管理する
	/// </summary>
	public class TileDataMap : IEnumerable<TileData>
    {
		#region 定数, class, enum

		public struct Enumerator : IEnumerator<TileData>
		{
			private readonly TileDataMap _list;
			private int _index;


			public Enumerator(TileDataMap list)
			{
				_list = list;
				_index = -1;
			}

			public TileData Current => _list.GetTile(_index);
			object IEnumerator.Current => _list.GetTile(_index);
			public  void Dispose() {}
			public bool MoveNext() => ++_index < _list.Size;
			public void Reset() => _index = 0;
		}

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		public int Width { get; private set; }
		public int Height { get; private set; }

		public int Size => Width * Height;

		public TileData[] Tiles { get; private set; }

		/// <summary>
		/// 部屋MAP
		/// つながってる部屋は同じ値が入る 1～
		/// </summary>
		public int[] RoomMapArray { get; private set; }
		public Dictionary<int, List<int>> RoomMap { get; } = new Dictionary<int, List<int>>();

		/// <summary>
		/// 道Map
		/// </summary>
		public int[] RoadMapArray { get; private set; }
		public Dictionary<int, List<int>> RoadMap { get; } = new Dictionary<int, List<int>>();

		/// <summary>
		/// 下り階段の場所
		/// </summary>
		public Vector2Int StepDownPos { get; private set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public IEnumerator<TileData> GetEnumerator() => new Enumerator(this);
		IEnumerator IEnumerable.GetEnumerator() => new Enumerator(this);

		public void Initialize(int width, int height)
		{
			Width = width;
			Height = height;

			Tiles = new TileData[width * height];

			for (int y = 0; y < Height; ++y)
			{
				for (int x = 0; x < Width; ++x)
				{
					var index = y * Width + x;

					Tiles[index].SetPos(x, y);
					Tiles[index].SetIndex(index);
				}
			}
		}

		public void AllTilesSetWall()
		{
			Tiles.ForEach((ref TileData tileData_) => tileData_.SetWall());
		}

		/// <summary>
		/// 部屋のマップを作成する
		/// </summary>
		public void BuildRoomMap() =>
			RoomMapArray = BuildMapInternal(RoomMap, TileFlag.Floor);

		/// <summary>
		/// 道のマップを作成する
		/// </summary>
		public void BuildRoadMap() =>
			RoadMapArray = BuildMapInternal(RoadMap, TileFlag.Road);


        /// <summary>
        /// 指定区画を指定フラグで上書きする
        /// </summary>
        public void FillRect(int left, int top, int right, int bottom, TileFlag flag, System.Predicate<TileData> predicate = null)
        {
            for (int y = top; y < bottom; ++y)
            {
                for (int x = left; x < right; ++x)
                {
					if (!InRange(x, y)) continue;

					ref var tileData = ref GetTile(x, y);

					// 許可されたところのみフラグを設定する
					if (predicate != null)
					{
						if (!predicate(tileData))
						{
							continue;
						}
					}

					tileData.SetFlag(flag);
                }
            }
        }

		/// <summary>
		/// 道を作成する。
		/// 途中に部屋と隣接する場合は上書きしない
		/// </summary>
		/// <param name="left"></param>
		/// <param name="top"></param>
		/// <param name="right"></param>
		/// <param name="bottom"></param>
		public void FillRectRoad(int left, int top, int right, int bottom, System.Predicate<TileData> predicate = null)
		{
			for (int y = top; y < bottom; ++y)
			{
				for (int x = left; x < right; ++x)
				{
					ref var tileData = ref GetTile(x, y);
					if (!predicate?.Invoke(tileData) ?? false) continue;

					// 部屋の場所は書き換えない
				//	if (tileData.HasFlag(TileFlag.Floor)) continue;

					// 部屋と隣接していたらtrueをいれる
					tileData.SetFlag(TileFlag.Road);
				}
			}
		}
		public void FillRectRoadReverse(int left, int top, int right, int bottom, System.Predicate<TileData> predicate = null)
		{
			for (int y = bottom - 1; y >= top; --y)
			{
				for (int x = right - 1; x >= left; --x)
				{
					ref var tileData = ref GetTile(x, y);
					if (!predicate?.Invoke(tileData) ?? false) continue;

					// 部屋の場所は書き換えない
					//	if (tileData.HasFlag(TileFlag.Floor)) continue;

					// 部屋と隣接していたらtrueをいれる
					tileData.SetFlag(TileFlag.Road);
				}
			}
		}

		/// <summary>
		/// 範囲内かどうか
		/// </summary>
		public bool InRange(int x, int y) =>
			x >= 0 && x < Width && y >= 0 && y < Height;

		/// <summary>
		/// [x, y] から指定したタイル情報を返す
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public ref TileData GetTile(int x, int y)
		{
			Utility.Log.Assert(x >= 0 && x <= Width && y >= 0 && y <= Height, "範囲から飛び出してます");

			return ref GetTile(y * Width + x);
		}
		public ref TileData GetTile(in Vector2Int pos) =>
			ref GetTile(pos.x, pos.y);

		public ref TileData GetTile(int index) =>
			ref Tiles[index];

		/// <summary>
		/// [x, y] から指定した<see cref="TileFlag"/>を返す
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public TileFlag GetTileFlag(int x, int y)
		{
			Utility.Log.Assert(x >= 0 && x <= Width && y >= 0 && y <= Height, "範囲から飛び出してます");
			return GetTileFlag(y * Width + x);
		}

		public TileFlag GetTileFlag(Vector2Int pos) =>
			GetTileFlag(pos.x, pos.y);

		public TileFlag GetTileFlag(Vector3Int pos) =>
			GetTileFlag(pos.x, pos.y);

		public TileFlag GetTileFlag(int index) =>
			GetTile(index).Flag;

		public void SetTileFlag(Vector2Int pos, TileFlag tileFlag)
		{
			ref var tileData = ref GetTile(pos.x, pos.y);
			tileData.SetFlag(tileFlag);
		}

		/// <summary>
		/// 下り階段の場所を設定する
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public void SetStepDownFlag(int x, int y)
		{
			StepDownPos = new Vector2Int(x, y);

			GetTile(x, y).AddFlag(TileFlag.StepDown);
		}

		public int GetRoomMapValue(int x, int y)
		{
			if (!InRange(x, y)) return -1;
			return RoomMapArray[y * Width + x];
		}

		public bool EqualRoomMapValue(int x, int y, int value) =>
			GetRoomMapValue(x, y) == value;

		/// <summary>
		/// 指定したTileFlagと隣接していたらtrue
		/// </summary>
		public bool IsAdjacent(int x, int y, TileFlag tileFlag) =>
			Builder.Common.CallDirection(x, y, (_x, _y) => GetTile(_x, _y).HasFlag(tileFlag));

		/// <summary>
		/// 部屋と隣接する場合true
		/// </summary>
		public bool IsRoomAdjacent(int x, int y) =>
			IsAdjacent(x, y, TileFlag.Floor);

		public bool IsRoomAdjacent(in Vector2Int pos) =>
			IsRoomAdjacent(pos.x, pos.y);

		/// <summary>
		/// 隣接しているtileFlagの数を返す
		/// </summary>
		public int GetAdjastNum(in Vector2Int pos, TileFlag tileFlag)
		{
			int result = 0;
			
			Builder.Common.CallDirection(pos.x, pos.y,
				(_x, _y) =>
				{ 
					if (GetTile(_x, _y).HasFlag(tileFlag))
					{
						++result;
					}
				});

			return result;
		}

		#endregion


		#region private 関数


		private int[] BuildMapInternal(Dictionary<int, List<int>> map, TileFlag tileFlag)
		{
			var mapArray = new int[Width * Height];
			map.Clear();

			void Scanning(int x, int y, int v, List<int> list)
			{
				if (!InRange(x, y)) return;

				var index = y * Width + x;

				// すでに値が入ってる場合
				if (mapArray[index] != 0) return;

				// 部屋以外の場合
				if (!Tiles[index].HasFlag(tileFlag)) return;

				mapArray[index] = v;
				list.Add(v);

				// 上下左右
				Scanning(x - 1, y, v, list);
				Scanning(x + 1, y, v, list);
				Scanning(x, y - 1, v, list);
				Scanning(x, y + 1, v, list);
			}

			int value = 0;

			for (int y = 0; y < Height; ++y)
			{
				for (int x = 0; x < Width; ++x)
				{
					// すでに値が入っていたら何もしない
					int index = y * Width + x;
					if (mapArray[index] != 0) continue;

					if (Tiles[index].HasFlag(tileFlag))
					{
						if (!map.TryGetValue(++value, out List<int> list))
						{
							list = new List<int>();
							map.Add(value, list);
						}

						Scanning(x, y, value, list);
					}
				}
			}

			return mapArray;
		}

		#endregion
	}
}
