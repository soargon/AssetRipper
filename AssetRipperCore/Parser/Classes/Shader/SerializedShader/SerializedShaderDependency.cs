﻿using AssetRipper.IO.Asset;

namespace AssetRipper.Parser.Classes.Shader.SerializedShader
{
	public struct SerializedShaderDependency : IAssetReadable
	{
		public void Read(AssetReader reader)
		{
			From = reader.ReadString();
			To = reader.ReadString();
		}

		public string From { get; set; }
		public string To { get; set; }
	}
}
