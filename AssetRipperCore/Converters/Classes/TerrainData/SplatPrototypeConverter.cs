﻿using AssetRipper.Converters.Project;
using AssetRipper.Parser.Classes;
using AssetRipper.Parser.Classes.TerrainData;

namespace AssetRipper.Converters.Classes.TerrainData
{
	public static class SplatPrototypeConverter
	{
		public static TerrainLayer GenerateTerrainLayer(IExportContainer container, ref SplatPrototype origin)
		{
			throw new System.NotImplementedException();
			/*TerrainLayer layer = new TerrainLayer();
			layer.DiffuseTexture = origin.Texture;
			layer.NormalMapTexture = origin.NormalMap;
			layer.TileSize = origin.TileSize;
			layer.TileOffset = origin.TileOffset;
			layer.Specular = new ColorRGBAf(origin.SpecularMetallic.X, origin.SpecularMetallic.Y, origin.SpecularMetallic.Z, 1.0f);
			layer.Metallic = origin.SpecularMetallic.W;
			layer.Smoothness = origin.Smoothness;
			return layer;*/
		}
	}
}
