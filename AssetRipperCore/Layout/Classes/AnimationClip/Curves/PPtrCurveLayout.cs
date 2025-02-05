﻿using AssetRipper.Converters.Game;
using AssetRipper.Parser.Classes.AnimationClip;
using AssetRipper.Parser.Classes.AnimationClip.Curves;

namespace AssetRipper.Layout.Classes.AnimationClip.Curves
{
	public sealed class PPtrCurveLayout
	{
		public PPtrCurveLayout(LayoutInfo info)
		{
			if (info.Version.IsGreaterEqual(2017))
			{
				IsAlignCurve = true;
			}
		}

		public static void GenerateTypeTree(TypeTreeContext context, string name)
		{
			PPtrCurveLayout layout = context.Layout.AnimationClip.PPtrCurve;
			context.AddNode(layout.Name, name);
			context.BeginChildren();
			context.AddArray(layout.CurveName, PPtrKeyframe.GenerateTypeTree);
			context.AddString(layout.AttributeName);
			context.AddString(layout.PathName);
			context.AddNode(TypeTreeUtils.TypeStarName, layout.ClassIDName, 1, sizeof(int));
			context.AddPPtr(context.Layout.MonoScript.Name, layout.ScriptName);
			context.EndChildren();
		}

		public int Version => 1;

		/// <summary>
		/// All versions
		/// </summary>
		public bool HasCurve => true;
		/// <summary>
		/// All versions
		/// </summary>
		public bool HasAttribute => true;
		/// <summary>
		/// All versions
		/// </summary>
		public bool HasPath => true;
		/// <summary>
		/// All versions
		/// </summary>
		public bool HasClassID => true;
		/// <summary>
		/// All versions
		/// </summary>
		public bool HasScript => true;

		/// <summary>
		/// 2017.1 and greater
		/// </summary>
		public bool IsAlignCurve { get; }

		public string Name => nameof(PPtrCurve);
		public string CurveName => "curve";
		public string AttributeName => "attribute";
		public string PathName => "path";
		public string ClassIDName => "classID";
		public string ScriptName => "script";
	}
}
