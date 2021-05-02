﻿//
// CharaStatus.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.08
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.Chara
{
	/// <summary>
	/// <see cref="ViewBase"/>キャラクターのHP等を管理するステイタス
	/// </summary>
	[System.Serializable]
	public class CharaStatus
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Header("現在のLv")]
		[SerializeField] private IntReactiveProperty _lv = default;

		[Header("HP")]
		[SerializeField] private CharaStatusValueObject _hp = default;

		[Header("スタミナ")]
		[SerializeField] private CharaStatusValueObject _stamina = default;

		[Header("ちから")]
		[SerializeField] private CharaStatusValueObject _power = default;

		#endregion


		#region プロパティ

		/// <summary>
		/// 現在のレベル
		/// </summary>
		public ReadOnlyReactiveProperty<int> Lv =>  _lv.ToReadOnlyReactiveProperty();

		/// <summary>
		/// HP
		/// </summary>
		public CharaStatusValueObject HP => _hp;

		/// <summary>
		/// スタミナ
		/// </summary>
		public CharaStatusValueObject Stamina => _stamina;

		/// <summary>
		/// ちから
		/// </summary>
		public CharaStatusValueObject Power => _power;


		/// <summary>
		/// 死んだとき(HPが0)に通知を受ける
		/// </summary>
		public ReadOnlyReactiveProperty<bool> IsDead { get; private set; }

		#endregion


		#region コンストラクタ, デストラクタ

		public CharaStatus(long hp, long stamina, int power)
		{
			_lv = new IntReactiveProperty(1);

			_hp = new CharaStatusValueObject(hp, hp);
			_power = new CharaStatusValueObject(power, power);
			IsDead = _hp.Current.Select(hp_ => hp_ <= 0).ToReadOnlyReactiveProperty();

			_stamina = new CharaStatusValueObject(stamina, stamina);
		}

		public CharaStatus(MasterData.Chara.StatusData statusData)
			: this(statusData.HP, statusData.Stamina, statusData.Power)
		{
		}

		public void SetLv(int lv)
		{
			_lv.Value = lv;
		}

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
