﻿//
// MoveAIData.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.15
//

using UnityEngine;
using Ling.Utility.Attribute;

namespace Ling.MasterData.Chara
{
	using MoveAI = Ling.AI.Move;

	/// <summary>
	/// 移動AIデータ
	/// </summary>
	public class MoveAIData
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField, FieldName("移動AIの種類")]
		private Const.MoveAIType _moveAIType = default;

		[SerializeField, FieldName("移動AIパラメータ１")]
		private int _moveAIParam1 = default;

		[SerializeField, FieldName("穴を無視する(浮遊系)")]
		private bool _isHoleIgnore = default;

		[SerializeField, FieldName("移動AIの最も優先すべきターゲット")]
		private Const.MoveAITarget _firstTarget = default;

		[SerializeField, FieldName("移動AIの二番目に優先すべきターゲット")]
		private Const.MoveAITarget _secondTarget = default;

		#endregion


		#region プロパティ

		public Const.MoveAIType MoveAIType => _moveAIType;

		public int MoveAIParam1 => _moveAIParam1;

		public bool IsHoleIgnore => _isHoleIgnore;

		public Const.MoveAITarget FirstTarget => _firstTarget;

		public Const.MoveAITarget SecondTarget => _secondTarget;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public MoveAI.MoveAIFactory CreateFactory() =>
			new MoveAI.MoveAIFactory(this);

		#endregion


		#region private 関数

		#endregion
	}
}
