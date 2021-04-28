﻿//
// FoodMaster.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.11.08
//

using UnityEngine;
using Utility.Attribute;

namespace Ling.MasterData.Item
{
	/// <summary>
	/// 食べ物Master
	/// </summary>
	[CreateAssetMenu(menuName = "MasterData/FoodMaster", fileName = "FoodMaster")]
	public class FoodMaster : ItemMaster
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField, FieldName("種類")]
		private Const.Item.Food _type = default;

		[SerializeField, FieldName("スタミナ回復量")]
		private int _staminaValue = 0;

		[SerializeField, FieldName("HP回復量")]
		private int _hpValue = 0;

		#endregion


		#region プロパティ

		/// <summary>
		/// アイテムカテゴリ
		/// </summary>
		public override Const.Item.Category Category => Const.Item.Category.Food;

		public Const.Item.Food Type => _type;
		public int StaminaValue => _staminaValue;
		public int HPValue => _hpValue;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
