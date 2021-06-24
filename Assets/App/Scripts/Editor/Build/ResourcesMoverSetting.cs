//
// ResourcesMoverSetting.cs
// ProductName Ling
//
// Created by dearp on 2020.02.23
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Editor.Build
{
	/// <summary>
	/// <see cref="ResourcesMover"/>の設定が保存されているEditorスクリプト
	/// </summary>
	[CreateAssetMenu(menuName = "Tools/ResourcesMover")]
	public class ResourcesMoverSetting : ScriptableObject
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		public string destinationPath;		// 移動先フォルダ
		public List<string> sourceFolders;	// 移動するフォルダリスト

		#endregion


		#region プロパティ


		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
