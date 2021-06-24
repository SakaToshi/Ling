//
// ResourcesMover.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.02.23
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

namespace Ling.Editor.Build
{
	/// <summary>
	/// Releaseビルド時に特定フォルダを自動的に移動させる
	/// </summary>
	public class ResourcesMover : IPreprocessBuildWithReport, IPostprocessBuildWithReport
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private ResourcesMoverSetting _setting;

		#endregion


		#region プロパティ

		/// <summary>
		/// 実行順序 値が小さい方から実行される
		/// </summary>
		public int callbackOrder => 0;


		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 設定が保存されているScriptableObjectのインスタンスを取得する
		/// </summary>
		/// <returns></returns>
		public static ResourcesMoverSetting GetSetting()
		{
			var guid = AssetDatabase.FindAssets("t:" + nameof(ResourcesMoverSetting)).FirstOrDefault();
			var filePath = AssetDatabase.GUIDToAssetPath(guid);
			if (string.IsNullOrEmpty(filePath))
			{
				throw new System.IO.FileNotFoundException("MyScriptableObject does not found");
			}

			return AssetDatabase.LoadAssetAtPath<ResourcesMoverSetting>(filePath);
		}


		public void OnPreprocessBuild(BuildReport report)
		{
			if (IsDevelopment(report)) return;

//			Directory.Move("", "");
//			File.Move(".meta", ".meta");
		}

		public void OnPostprocessBuild(BuildReport report)
		{
			if (IsDevelopment(report)) return;

			_setting = GetSetting();

			if (_setting == null) return;

			foreach(var elm in _setting.sourceFolders)
			{

			}

//			Directory.Move("", "");
//			File.Move(".meta", ".meta");
		}

		#endregion


		#region private 関数


		private bool IsDevelopment(BuildReport report)
		{
			return (report.summary.options & UnityEditor.BuildOptions.Development) != 0;
		}


		#endregion
	}
}
