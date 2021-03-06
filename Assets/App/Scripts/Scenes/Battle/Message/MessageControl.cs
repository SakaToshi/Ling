﻿//
// MessageControl.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.08
//

using Utility;
using UnityEngine;
using UniRx;

using Zenject;

namespace Ling.Scenes.Battle.Message
{
	/// <summary>
	/// メッセージ周りをコントロールする
	/// </summary>
	public class MessageControl : MonoBehaviour
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private MessageView _view = null;
		[SerializeField] private MessageSelect _select = null;

		[Inject] private IEventManager _eventManager = null;
		[Inject] private BattleManager _battleManager;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Clear()
		{
			_view.Clear();
		}

		public void ShowMessage(string text)
		{
			_view.SetText(text);
		}

		#endregion


		#region private 関数

		private void Start()
		{
			// 「メッセージ表示可能状態」と「メッセージ送信中」をリンクさせる
			_view.CanNextTextShow
				.Subscribe(canNextTextShow => 
				{
					_battleManager.IsMessageSending = !canNextTextShow;
				});

			_eventManager.Add<EventMessageText>(this,
				ev_ =>
				{
					_view.SetText(ev_.text);
				});

			_eventManager.Add<EventMessageTextSelect>(this,
				ev_ =>
				{
					var onSelected = ev_.onSelected;

					// メッセージ表示後選択肢表示
					_view.SetText(ev_.text,
						() =>
						{
							_select.onSelected = selectIndex_ =>
							{
								// 選択された後は基本テキスト消す
								Clear();

								onSelected?.Invoke(selectIndex_);
							};

							_select.Show(ev_.selectTexts);
						});
				});
		}

		private void OnDestroy()
		{
			_eventManager.RemoveAll(this);
		}

		#endregion
	}
}
