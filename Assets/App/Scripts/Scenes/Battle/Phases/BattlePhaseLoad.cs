﻿//
// BattlePhaseLoad.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.01
//

using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.Scenes.Battle.Phases
{
	/// <summary>
	/// 
	/// </summary>
	public class BattlePhaseLoad : BattlePhaseBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private PoolManager _poolManager;
		[Inject] private Map.MapManager _mapManager = null;
		[Inject] private Chara.CharaManager _charaManager = null;
		[Inject] private Common.Scene.IExSceneManager _sceneManager = default;

		private bool _isFinish = false;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ


		#endregion


		#region public, protected 関数

		protected override void AwakeInternal()
		{
		}

		public override void PhaseStart()
		{
			// タイルのプールを作成する
			// とりあえず最大の数作ってみる
			LoadAsync().Forget();
		}

		public override void PhaseUpdate()
		{
			if (!_isFinish) return;

			Change(Phase.FloorSetup);
		}

		public override void PhaseStop()
		{
			_isFinish = false;
		}

		#endregion


		#region private 関数

		private async UniTask LoadAsync()
		{
			// 最初のマップ作成
			_mapManager.Setup(_model.StageMaster);

			await _mapManager.BuildMapAsync(1, 2);

			// 1階層目を開始地点とする
			_mapManager.SetCurrentMap(1);

			// キャラクタのセットアップ処理
			await _charaManager.InitializeAsync();

			_charaManager.SetStageMaster(_model.StageMaster);

			var builder = _mapManager.CurrentMapData.Builder;
			var playerPos = builder.GetPlayerInitPosition();

			// プレイヤーにMap情報を初期座標を設定
			var mapControl = Scene.MapControl;

			var player = _charaManager.Player;
			mapControl.SetChara(player);

			player.Model.InitPos(playerPos);

			// 初期マップの敵を生成する
			await _charaManager.BuildEnemyGroupAsync(1, _mapManager.FindGroundTilemap(1));
			await _charaManager.BuildEnemyGroupAsync(2, _mapManager.FindGroundTilemap(2));

			// 敵をマップに配置する
			Scene.DeployEnemyToMap(_charaManager.FindEnemyControlGroup(1), 1);
			Scene.DeployEnemyToMap(_charaManager.FindEnemyControlGroup(2), 2);

			// Player ステイタスUIを表示する
			// todo: シーンの依存関係に紐付けたい
//			await _sceneManager.AddSceneAsync<Status.StatusScene>(Common.Scene.SceneID.Status, argument: null);


			_isFinish = true;
		}

		#endregion
	}
}