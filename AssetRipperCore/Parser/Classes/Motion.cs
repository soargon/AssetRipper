﻿using AssetRipper.Layout;
using AssetRipper.Parser.Asset;

namespace AssetRipper.Parser.Classes
{
	public abstract class Motion : NamedObject
	{
		protected Motion(AssetLayout layout) : base(layout) { }

		protected Motion(AssetInfo assetInfo) : base(assetInfo) { }
	}
}
