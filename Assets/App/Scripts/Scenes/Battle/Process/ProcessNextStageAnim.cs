﻿//
// ProcessNextStageAnim.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.28
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
using UnityEngine.UI;

using Zenject;

namespace Ling.Scenes.Battle.Process
{
	/// <summary>
	/// 階層を移動するときのアニメーション
	/// </summary>
	public class ProcessNextStageAnim : Utility.ProcessBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private Map.MapManager _mapManager = null;
		private Chara.CharaManager _charaManager = null;
		private MasterData.MasterManager _masterManager = null;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		protected override void ProcessStartInternal()
		{
			_mapManager = _diContainer.Resolve<Map.MapManager>();
			_charaManager = _diContainer.Resolve<Chara.CharaManager>();
			_masterManager = _diContainer.Resolve<MasterData.MasterManager>();

			MoveAsync().Forget();
		}

		#endregion


		#region private 関数

		private async UniTaskVoid MoveAsync()
		{
			var mapControl = _mapManager.MapControl;

			await
			(
				mapControl.MoveUpAsync(), 
				PlayerMoveAsync()
			);

			ProcessFinish();
		}

		private async UniTask PlayerMoveAsync()
		{
			var player = _charaManager.PlayerView;
			var localPos = player.transform.localPosition;
			var constMaster = _masterManager.Const;

			await player.transform.DOLocalMoveZ(constMaster.MapDiffHeight, constMaster.PlayerLevelMoveTime);
		}

		#endregion
	}
}
