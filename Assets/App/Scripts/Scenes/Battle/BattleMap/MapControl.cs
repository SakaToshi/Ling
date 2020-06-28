﻿//
// MapControl.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.03
//

using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

using Zenject;

namespace Ling.Scenes.Battle.BattleMap
{
	/// <summary>
	/// ダンジョンマップコントロール
	/// </summary>
	public class MapControl : MonoBehaviour
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private MapView _view;

		[Inject] private MasterData.MasterManager _masterManager = null;

		private MapModel _model;
		private Common.Tile.MapTile _mapTile;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void SetModel(MapModel model) =>
			_model = model;

		public void Startup(int curretMapIndex)
		{
			_model.ChangeMapByIndex(curretMapIndex);

			// 見た目の更新
			_view.OnStartItem = _view.OnUpdateItem = 
				(groundMap_, mapIndex_) =>
				{
					var mapData = _model.FindMapData(mapIndex_);
					if (mapData == null)
					{
						Utility.Log.Error($"マップデータが見つからない {mapIndex_}");
						return;
					}

					groundMap_.BuildMap(mapIndex_, mapData.Width, mapData.Height, mapData.MapTileRenderData);
				};
			
			_view.Startup(_model, curretMapIndex, 40);
		}

		/// <summary>
		/// 次マップの作成と移動を行う
		/// </summary>
		/// <param name="nextMapIndex"></param>
		/// <param name="createMapIndex"></param>
		/// <returns></returns>
		public IObservable<AsyncUnit> CreateMapView(int nextMapIndex, int createMapIndex)
		{
			return _view.CreateMapView(nextMapIndex, createMapIndex);
		}

		/// <summary>
		/// 現在のマップを一つ進め、変更する
		/// </summary>
		public void ChangeMap(int nextMapIndex)
		{
			// 現在のMapIndexを変更する
			_model.ChangeMapByIndex(nextMapIndex);

			_view.SetMapIndex(nextMapIndex);

			// もう見えないMapを削除する
			_view.RemoveExtraTilemap(_model.ShowMapIndexes);

			// 変化先のマップを中心とする
			_view.ForceTransformAdjustment(nextMapIndex);

			// プレイヤーの座標も初期化する
		}

		/// <summary>
		/// 指定したセルの中心ワールド座標を取得する
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public Vector3 GetCellCenterWorld(int mapIndex, int x, int y)
		{
			var tilemap = FindTilemap(mapIndex);

			return tilemap.GetCellCenterWorld(new Vector3Int(x, y, 0));
		}

		/// <summary>
		/// 指定階層のTilemapを取得する
		/// </summary>
		/// <param name="mapIndex"></param>
		/// <returns></returns>
		public Tilemap FindTilemap(int mapIndex) =>
			_view.FindTilemap(mapIndex);

		/// <summary>
		/// 次のフロアに移動させる
		/// </summary>
		/// <remarks>
		/// MapView全体を上げる
		/// </remarks>
		public async UniTask MoveUpAsync()
		{
			var constMaster = _masterManager.Const;

			await _view.transform.DOLocalMoveY(constMaster.MapDiffHeight, constMaster.MapLevelMoveTime);
		}

		/// <summary>
		/// 動いたViewのY座標をもとに戻す
		/// </summary>
		public void ResetViewUpPosition()
		{
			var localPos = _view.transform.localPosition;
			_view.transform.localPosition = new Vector3(localPos.x, 0f, localPos.z);
		}

#endregion


#region private 関数

#endregion
	}
}